﻿using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager)
            : base(manager)
        {
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToHomePage();
            InitContactCreation();
            FillContactForm(contact);
            SubmitContactCreation();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper RemoveContactFromGroup(ContactData toBeRemoved, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            SelectGroupFromDropdown(group.Name);

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(
                dr => dr.FindElements(By.Name("remove")).Count > 0);

            SelectContact(toBeRemoved.Id);
            CommitRemoveContactFromGroup();

            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(
                dr => dr.FindElements(By.CssSelector("div.msgbox")).Count > 0);

            return this;
        }

        public ContactHelper AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToHomePage();

            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(
                dr => dr.FindElements(By.CssSelector("div.msgbox")).Count > 0);
            return this;
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        private void SelectGroupFromDropdown(string groupName)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(groupName);
        }

        private void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        private void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        private void CommitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToHomePage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index]
                .FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string adress = cells[3].Text;
            string allPhones = cells[5].Text;


            return new ContactData(firstName, lastName)
            {
                Address = adress,
                AllPhones = allPhones
            };
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(index);
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");

            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");

            return new ContactData(firstName, lastName)
            {
                Address = address,
                HomePhone = homePhone,
                MobilePhone = mobilePhone,
                WorkPhone= workPhone,
                Email = email
            };
        }

        public string GetContactInformatonFromDetailsPage(int index)
        {
            manager.Navigator.GoToHomePage();
            manager.Navigator.GoToContactDetailsPage(index);

            string contactDetails = driver.FindElement(By.Id("content")).Text;
            return contactDetails.Trim();
        }

        public ContactHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(v);
            RemoveContact();
            AcceptAlert();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Remove(ContactData contactToRemove)
        {
            manager.Navigator.GoToHomePage();
            SelectContact(contactToRemove.Id);
            RemoveContact();
            AcceptAlert();
            manager.Navigator.GoToHomePage();
            return this;
        }

        public ContactHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(v);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();
            return this;
        }

        public ContactHelper Modify(ContactData oldData, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            InitContactModification(oldData.Id);
            FillContactForm(newData);
            SubmitContactModification();
            ReturnToContactsPage();
            return this;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name=entry]"));
                foreach (IWebElement element in elements)
                {
                    IWebElement lastName = element.FindElement(By.CssSelector("td:nth-child(2)"));
                    IWebElement firstName = element.FindElement(By.CssSelector("td:nth-child(3)"));
                    string id = element.FindElement(By.CssSelector("td:nth-child(1)"))
                        .FindElement(By.TagName("input")).GetAttribute("value");

                    contactCache.Add(new ContactData(firstName.Text, lastName.Text) {
                        Id = id
                    });
                }
            }

            return new List<ContactData>(contactCache);
        }

        public bool IsAnyContactPresent()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper InitContactModification(int index)
        {
            driver.FindElement(By.XPath("(//img[@alt='Edit'])[" + (index + 1) + "]")).Click();
            return this;
        }

        public ContactHelper InitContactModification(string id)
        {
            IWebElement tr = driver.FindElement(By.XPath(@"//tr[td/input[@id='" + id + "']]"));
            tr.FindElement(By.XPath(@".//img[@alt='Edit']")).Click();
            return this;
        }


        public ContactHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index  + "]")).Click();
            return this;
        }

        public ContactHelper SelectContact(string id)
        {
            driver.FindElement(By.Id(id)).Click();
            return this;
        }

        public ContactHelper ReturnToContactsPage()
        {
            driver.FindElement(By.LinkText("home page")).Click();
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }
    }
}
