# BlogApplication
Blog Application with ASP.NET MVC, WEB API Core 2.2, and EntityFrameworkCore

Blog Application with ASP.NET MVC, WEB API Core 2.2, and EntityFrameworkCore


1.	The Solution is distributed on 3 Projects
     - CorApplication - Web API Core with EntityFrameworkCor
	 - Blog.DTO for DTOs
	 - Blog.Web for ASP.NET MVC (FrontEnd)

2.	Other Unloaded Projects were more of a TRY (not in use)
3.	CorApplication
	 - ConnectionFactory provide class to create Context 
	 - CryptoHelper for hashing the password and validating it 
	 - AccountController has Login method for login functionality and simple token geration wit LoginResponse using concept fom JWT.
	 - ApplicationContext also contain Seed for two roles (Admin,User)
4.	Blog.Web 
	 - Out of 3 healpers classes in Helper Folder, SecurityHelper is used for token and sessions maintenance (used cookie)
	 - Used Bootstrap at most places to keep the UI visually acceptable
	 

Note:

1.	Change the Connection String in "appsettings.json" in CorApplication and Execute Update-Database command for Database Creation.
2.	Both CorApplication(Web APIs) and Blog.Web are using Local IIS and not IIS Express.



UPDATES:


a. The deletion was handled but unfortunately, I placed the condition wrongly (forgot !).
b. If the blog is deleted and any other user tries to access it, I redirect him to 404 Page.
c. On logout, I delete the cookie to handle and for any request, I am using the token from cookie to authenticate and authorize.
d. I also have implemented SignalR so whenever someone creates a blog, all other users get a notification. (simple implementation).
