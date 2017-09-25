using NUnit.Framework;
using OpenQA.Selenium;
using System.Text.RegularExpressions;
using System;

namespace WebAddressbookTests
{
    public class ContacHelper : HelperBase
    {
        public ContacHelper(ApplicationManager manager)
            :base(manager)
        {
        }

        public ContacHelper Remove(int v)
        {
            manager.Navigator.GoToHomePage();
            if (!IsAnyContactPresent())
            {
                InitContactCreation()
                .FillContactForm(new ContactData("qqq", "111"))
                .SubmitContactCreation();
                manager.Navigator.GoToHomePage();
            }
            SelectContact(v);
            RemoveContact();
            AcceptAlert();
            return this;
        }

        public ContacHelper Modify(int v, ContactData newData)
        {
            manager.Navigator.GoToHomePage();
            if (!IsAnyContactPresent())
            {
                InitContactCreation()
                .FillContactForm(new ContactData("qqq", "111"))
                .SubmitContactCreation();
                manager.Navigator.GoToHomePage();
            }
            SelectContact(v);
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            return this;
        }

        private bool IsAnyContactPresent()
        {
            return IsElementPresent(By.Name("selected[]"));
        }

        public ContacHelper SubmitContactModification()
        {driver.FindElement(By.Name("update")).Click();
            return this;
        }

        public ContacHelper InitContactModification()
        {
            driver.FindElement(By.CssSelector("img[alt=\"Edit\"]")).Click();
            return this;
        }

        public ContacHelper InitContactCreation()
        {
            driver.FindElement(By.LinkText("add new")).Click();
            return this;
        }
        public ContacHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            return this;
        }
        public ContacHelper SubmitContactCreation()
        {
            driver.FindElement(By.Name("submit")).Click();
            return this;
        }

        public ContacHelper SelectContact(int index)
        {
            driver.FindElement(By.XPath("(//input[@name='selected[]'])[" + index + "]")).Click();
            return this;
        }

        public ContacHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            return this;
        }
    }
}
