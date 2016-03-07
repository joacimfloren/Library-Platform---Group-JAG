﻿using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Mockup;

namespace JAGLibrary.Controllers
{
    public class HomeController : Controller
    {
        Mockup mup = new Mockup();

        public ActionResult Index()
        {
            var model = new Search();

            return View("Index", "_StandardLayout", model);
        }

        public ActionResult Browse()
        {
            var br = new Browse();
            var s = new Search();
            br._search = s;
            var model = new BrowseResult();
            model._browse = br;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult BrowseBy(string b)
        {
            var br = new Browse();
            var s = new Search();
            br._search = s;
            br._browseBy = b;
            var model = new BrowseResult();
            model._browse = br;

            return View("Browse", "_BrowseLayout", model);
        }

        public ActionResult BrowseLetterBy(string s, string by)
        {
            if (by == "Author")
            {
                var model = new BrowseResult();
                var b = new Browse();
                b._browseBy = by;
                model._browse = b;
                model._letter = s;
                model._aList = Service.Services.AuthorServices.getAuthorListFromFirstLetter(s);

                return View("BrowseResult", "_BrowseLayout", model);
            }

            else if (by == "Book")
            {
                var model = new BrowseResult();
                var b = new Browse();
                b._browseBy = by;
                model._browse = b;
                model._letter = s;

                model._bList = Service.Services.BookServices.getBookListOnFirstLetter(s);

                return View("BrowseResult", "_BrowseLayout", model);
            }

            return View();
        }

        public ActionResult Book()
        {
            List<BookDetails> bdList = Service.Services.BookServices.getBookDetailsFromIsbn("0137696396");
            var model = new BookDetails();

            bool fixString = false;
            foreach (BookDetails bd in bdList) {
                if (fixString)
                    model.authorstring = model.authorstring + ", " + bd.author_firstname + " " + bd.author_lastname;
                else {
                    model.authorstring = bd.author_firstname + " " + bd.author_lastname;
                    fixString = true;
                }
            }

            model.book_isbn = bdList[0].book_isbn;
            model.book_title = bdList[0].book_title;
            model.book_signId = bdList[0].book_signId;
            model.book_publicationYear = bdList[0].book_publicationYear;
            model.book_publicationInfo = bdList[0].book_publicationInfo;
            model.book_pages = bdList[0].book_pages;
            
            return View("Book", "_StandardLayout", model);
        }

        public ActionResult Login()
        {
            var model = new LoginData();

            return View("Login", "_StandardLayout", model);
        }

        public ActionResult Logout() 
        {
            Session.Clear();
            var model = new LoginData();
            return View("Login", "_StandardLayout", model);
        }

        //[HttpGet]
        public ActionResult SearchFunc(Common.Models.Search m)
        {
            //skicka in sökdata
            var sr = new SearchResult();
            m._searchResult = sr;

            return View("SearchResult", "_StandardLayout", m);
        }

        public string getHash(string str, string salt)
        {
            string hash = str + salt;
            return hash.GetHashCode().ToString(); ;
        }

        //[HttpGet]
   
        public ActionResult LoginFunc(Common.Models.LoginData m)
        {
            
            Mockup mockup = new Mockup();
            
            if (mockup.userList.Exists(x => x._username == m._username))
            {
                m._hash = getHash("123",  mockup.userList.Find(x => x._username == m._username)._salt);
                
                if (mockup.userList.Find(x => x._username == m._username)._hash == getHash(m._password, mockup.userList.Find(x => x._username == m._username)._salt))
                {
                    switch (mockup.userList.Find(x => x._username == m._username)._level)
                    {
                        case "1":
                            //Session["level"]="Borrower";
                            Session["pId"] = m._username;
                            return Redirect("/Borrower/Borrower/");
        
                        case "2":
                            Session["level"] = "Admin";
                            Session["pId"] = "Admin";
                            //return View("../Admin/Admin", "_StandardLayout");
                            return Redirect("/Admin/Admin/");

                            
                        default:
                            break;
                    }
                }
            }
            
            else
            {

            }

            return View("Login", "_StandardLayout");
        }
    }
}