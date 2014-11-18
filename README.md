FoodShare
==================

Telerik Academy ASP.NET MVC Project

This is a charity website which goal is to connect donors and recipients, and to smooth the donation process. There are 4 main sections in the website:

- Public 

On the home page there is a list with latest 10 donations and most contributing donors. When clicking on Donations in the navigation menu they go to a page with the donations sorted by category. Also from the public part donors and recipients can register. 

- Donors 

Log in and add a donation from 'My Donations' area. In the same area they are able to see a list with their all donations up to now. From the Grid view they can edit some of the details on the donations, for detailed 'edit' they should click on the Edit button next to each donation in the grid.   

- Recipients

Recipients register from the public section of the website, click on Donations, then Details and fill in the request form. From "My Requests" section they are able to see their all requests up to now. From the Unread Comments link they can add comment to the request to a donation they have already made.

- Administration

Administrators are able to see a list of all donors, recipients, donations and donation requests in Grid list View. They can also edit the details of all these entities.

----------------------

Technical details: 

Layers: 

Data Layer (Entities, Repositories)

Application (Services) Layer

Web Project (MVC) - Areas, Controllers, ViewModels, Views, Display Templates, Editor Templates, Caching, Ajax, Kendo DatePicker and Kendo Grid.


Used Libraries: Ninject (Dependency Inversion), AutoMapper (Entities-to-ViewModels), PagedList.MVC (Pager), Telerik UI for ASP.NET MVC (Kendo)