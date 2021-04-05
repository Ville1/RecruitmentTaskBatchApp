using RecruitmentTaskBatchApp.Data;
using RecruitmentTaskBatchApp.Data.DB.Model;
using RecruitmentTaskBatchApp.Data.Repository;
using RecruitmentTaskBatchApp.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RecruitmentTaskBatchApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<FileData> data = FileManager.Read();
            List<EmailModel> tenAttributes = new List<EmailModel>();
            foreach (FileData fileData in data) {
                //Update DB
                foreach (EmailData emailData in fileData.Emails) {
                    EmailModel existingEmailData = Repository.Emails.Get(emailData.Key);
                    if(existingEmailData == null) {
                        //Create a new entry
                        EmailModel newEmail = new EmailModel() {
                            EmailKey = emailData.Key,
                            Email = emailData.Email,
                            Created = DateTime.Now
                        };
                        //Attributes
                        foreach (string attribute in emailData.Attributes.Distinct()) {
                            newEmail.Attributes.Add(GetAttribute(attribute));
                        }
                        //Save email row
                        Repository.Emails.Save(newEmail);
                        //Save attribute links
                        foreach(AttributeData attribute in newEmail.Attributes) {
                            Repository.EmailAttributes.Save(new EmailAttribute() {
                                EmailKey = newEmail.EmailKey,
                                AttributeId = attribute.Id
                            });
                        }
                        if(newEmail.Attributes.Count >= 10) {
                            tenAttributes.Add(newEmail);
                        }
                    } else {
                        //Update existing email
                        int oldAttributeCount = existingEmailData.Attributes.Count;
                        //Attributes
                        List<AttributeData> newAttributes = new List<AttributeData>();
                        foreach (string attribute in emailData.Attributes.Distinct()) {
                            if(!existingEmailData.Attributes.Exists(x => x.Name == attribute)) {
                                AttributeData attributeData = GetAttribute(attribute);
                                existingEmailData.Attributes.Add(attributeData);
                                newAttributes.Add(attributeData);
                            }
                        }
                        if(existingEmailData.Email != emailData.Email) {
                            //Email changed
                            existingEmailData.Email = emailData.Email;
                            Repository.Emails.Save(existingEmailData);
                        }
                        //Save attribute links
                        foreach(AttributeData attributeData in newAttributes) {
                            Repository.EmailAttributes.Save(new EmailAttribute() {
                                EmailKey = existingEmailData.EmailKey,
                                AttributeId = attributeData.Id
                            });
                        }
                        if (oldAttributeCount < 10 && existingEmailData.Attributes.Count >= 10) {
                            tenAttributes.Add(existingEmailData);
                        }
                    }
                }
                if (Config.ArchiveOldFiles) {
                    //Move file to archive folder
                    try {
                        File.Move(fileData.FullName, string.Format("{0}\\{1}", Config.ArchiveLocation, fileData.FullName.Substring(fileData.FullName.LastIndexOf('\\') + 1)));
                    } catch(Exception exception) {
                        Logger.Log(exception);
                        throw exception;
                    }
                }
            }
            foreach(EmailModel email in tenAttributes) {
                SendTenAttributesEmail(email);
            }
        }

        private static AttributeData GetAttribute(string name)
        {
            AttributeData attributeData = Repository.Attributes.GetAll().FirstOrDefault(x => x.Name == name);
            if (attributeData == null) {
                //Attribute does not exist -> create a new attribute
                attributeData = new AttributeData() { Name = name };
                Repository.Attributes.Save(attributeData);
                attributeData = Repository.Attributes.GetAll().First(x => x.Name == name);
            }
            return attributeData;
        }

        private static void SendTenAttributesEmail(EmailModel email)
        {
            SendGridClient client = new SendGridClient(Config.SendGridAPIKey);
            EmailAddress sender = new EmailAddress(Config.SendGridEmail, Config.SendGridEmailName);
            EmailAddress receiver = new EmailAddress(email.Email);
            SendGridMessage message = MailHelper.CreateSingleEmail(sender, receiver, Config.SendGridSubject, string.Join(", ", email.Attributes.Select(x => x.Name).ToArray()), null);
            Response response = client.SendEmailAsync(message).Result;
            if (!response.IsSuccessStatusCode) {
                Logger.Log(string.Format("Sending email to {0} failed with status code {1}", email.Email, response.StatusCode.ToString()));
            }
        }
    }
}
