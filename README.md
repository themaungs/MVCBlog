# MVCBlog
Blog Site Based on ASP.NET MVC

Simple blog site created and designed by Min Maung & Lwin Maung. Contains features such as creating and managing pages. And having page details. Blogs and pages can be created with simple built in browser editor with the use of TinyMCE. 

Disqus can be turned on by filling in Disqus shortname value in Web.config.


To build the database initially do the following steps.
1. Open Package Manager Console
2. Select Default Project "MyBlogSite.Domain" from drop down.
3. run this command: 
Update-Database -StartUpProjectName "MyBlogSite"
