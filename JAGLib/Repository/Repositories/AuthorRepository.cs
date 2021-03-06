﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.EntityModel;
using System.Data.SqlClient;

namespace Repository.Repositories
{
    public class AuthorRepository
    {
        static private List<author> dbGetAuthorList(string query, SqlParameter[] sp)
        {
            List<author> _authorList = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    _authorList = new List<author>();
                    while (dar.Read())
                    {
                        author authObj = new author();
                        authObj._id = (int)dar["Aid"];
                        authObj._firstname = dar["FirstName"] as string;
                        authObj._lastname = dar["LastName"] as string;
                        authObj._birthyear = dar["BirthYear"] as string;
                        _authorList.Add(authObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _authorList;
        }

        static private authordetails dbGetBooksFromAuthor(string query, SqlParameter[] sp)
        {
            authordetails _author = null;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    authordetails authObj = new authordetails();
                    while (dar.Read())
                    {
                        authObj.author_firstname = dar["FirstName"] as string;
                        authObj.author_lastname = dar["LastName"] as string;
                        authObj.author_birthyear = dar["BirthYear"] as string;
                        
                        book b = new book();
                        b._isbn = dar["ISBN"] as string;
                        b._title = dar["Title"] as string;
                        authObj._bookList.Add(b);
                    }
                    _author = authObj;
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _author;
        }

        static private author dbOneAuthorFromAid(string query, SqlParameter[] sp)
        {
            author _author = new author();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        _author._id = (int)dar["Aid"];
                        _author._firstname = dar["FirstName"] as string;
                        _author._lastname = dar["LastName"] as string;
                        _author._birthyear = dar["BirthYear"] as string;
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return _author;
        }

        static private List<bookauthor> dbGetAllIsbnFromAuthor(string query, SqlParameter[] sp)
        {
            List<bookauthor> _baList = new List<bookauthor>();
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        bookauthor boObj = new bookauthor();
                        boObj._aid = (int)dar["Aid"];
                        boObj._isbn = dar["ISBN"] as string;
                        _baList.Add(boObj);
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
            
            return _baList;
        }

        static private int dbCountAuthorsOnIsbn(string query, SqlParameter[] sp)
        {
            int c = 0;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        c = (int)dar["NoOf"];
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return c;
        }

        static private int dbBookAuthorFromFromIsbn(string query, SqlParameter[] sp)
        {
            int i = 0;
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                SqlDataReader dar = cmd.ExecuteReader();
                if (dar != null)
                {
                    while (dar.Read())
                    {
                        i = (int)dar["Aid"];
                    }
                }
            }
            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }

            return i;
        }

        static private void dbInsert(string query, SqlParameter[] sp) 
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static private void dbRemoveOrEdit(string query, SqlParameter[] sp)
        {
            string _connectionString = DataSource.getConnectionString("projectmanager");
            SqlConnection con = new SqlConnection(_connectionString);
            SqlCommand cmd = new SqlCommand(query, con);

            if (sp != null && sp.Length > 0)
                cmd.Parameters.AddRange(sp);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }

            catch (Exception eObj) { throw eObj; }
            finally { if (con != null) con.Close(); }
        }

        static public List<author> dbGetAllAuthorList()
        {
            return dbGetAuthorList("SELECT * FROM author;", null);
        }

        static public List<author> dbGetAuthorListFromFirstletter(string c)
        {
            return dbGetAuthorList("SELECT * FROM author WHERE LastName LIKE @FIRST + '%' ORDER BY LastName;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@FIRST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = c
                }
            });
        }

        static public authordetails dbBooksFromAuthor(int aid)
        {
            return dbGetBooksFromAuthor("SELECT * FROM AUTHOR LEFT JOIN BOOK_AUTHOR ON AUTHOR.Aid = BOOK_AUTHOR.Aid LEFT JOIN BOOK ON BOOK_AUTHOR.ISBN = BOOK.ISBN WHERE AUTHOR.Aid = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public author dbGetAuthorFromAid(int aid)
        {
            return dbOneAuthorFromAid("SELECT * FROM AUTHOR WHERE Aid = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public List<bookauthor> dbGetBookIsbnFromAuthor(int aid)
        {
            return dbGetAllIsbnFromAuthor("SELECT * FROM BOOK_AUTHOR WHERE Aid = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public int dbCountAuthorsForBook(string isbn)
        {
            return dbCountAuthorsOnIsbn("SELECT COUNT(*) AS NoOf FROM BOOK_AUTHOR WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public int dbGetBookAuthorOfBook(string isbn)
        {
            return dbBookAuthorFromFromIsbn("SELECT * FROM BOOK_AUTHOR WHERE ISBN LIKE @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public void dbAddAuthor(author a) 
        {
            dbInsert("INSERT INTO AUTHOR (FirstName, LastName, BirthYear) VALUES (@FIRST, @LAST, @BIRTH);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@FIRST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._firstname
                },
                new SqlParameter() {
                    ParameterName = "@LAST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._lastname
                },
                new SqlParameter() {
                    ParameterName = "@BIRTH",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._birthyear
                }
            });
        }

        static public void dbAddBookAuthor(string isbn, int aid)
        {
            dbInsert("INSERT INTO BOOK_AUTHOR VALUES (@ISBN, @ID);", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                },
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public void dbRemoveAuthor(int aid)
        {
            dbRemoveOrEdit("DELETE FROM AUTHOR WHERE Aid = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public void dbRemoveBookAuthor(int aid)
        {
            dbRemoveOrEdit("DELETE FROM BOOK_AUTHOR WHERE Aid = @ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = aid
                }
            });
        }

        static public void dbRemoveBookAuthor(string isbn)
        {
            dbRemoveOrEdit("DELETE FROM BOOK_AUTHOR WHERE ISBN = @ISBN;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@ISBN",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = isbn
                }
            });
        }

        static public void dbEditAuthor(author a)
        {
            dbRemoveOrEdit("UPDATE AUTHOR SET FirstName=@FIRST, LastName=@LAST, BirthYear=@BIRTH WHERE Aid=@ID;", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@FIRST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._firstname
                },
                new SqlParameter() {
                    ParameterName = "@LAST",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._lastname
                },
                new SqlParameter() {
                    ParameterName = "@BIRTH",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = a._birthyear
                },
                new SqlParameter() {
                    ParameterName = "@ID",
                    SqlDbType = System.Data.SqlDbType.Int,
                    Value = a._id
                }
            });
        }

        static public List<author> dbSearchAuthor(string s)
        {
            return dbGetAuthorList("SELECT * FROM AUTHOR WHERE FirstName LIKE '%' + @SEARCH + '%' OR LastName LIKE '%' + @SEARCH + '%';", new SqlParameter[] {
                new SqlParameter() {
                    ParameterName = "@SEARCH",
                    SqlDbType = System.Data.SqlDbType.NVarChar,
                    Value = s
                }
            });
        }
    }
}