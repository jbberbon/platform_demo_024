# TEST TASK TO COMPLETE

- Share a link to a new GitHub repository with recruiting@logicimtech.com.
- The GitHub repository name is: `platform_demo_024`.
- The GitHub repository will contain a single .NET solution and a `README.md` file.

## .NET Solution

The solution name is `PlatformDemo`. It will contain 2 projects: a .NET 8 Class Library and an ASP.NET Core 8 Web App.

### .NET 8 Class Library
- One Entity Framework DbContext using a localdb, including a set of `ServicePlans` and a set of `Timesheets`
- `ServicePlan` class (`ServicePlanId`, `DateOfPurchase`)
- `Timesheet` class (`TimesheetId`, related service plan, Date and time of start, Date and time of end, Description)
- A service plan can have 0 or more timesheets associated with it.

### ASP.NET Core 8 Web App
- Reference the class library and build a page that shows the list of service plans along with their date of purchase and number of timesheets. Each service plan must show on a single line, even if it does not have related timesheets.
- Seed sample data (10-15 service plans, 0-5 timesheets per service plan)

## Additional Instructions
- `README.md`: copy the content of this task description in the readme file
- Document your code with proper comments
- Make it your own. Add something to the final solution to express your capabilities. It can be visual or functional.
- If you have a question about something not specified, use your judgment and explain your decision in code comments.
