
using center_of_school.data;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using twyn_machinia.validator;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<mongodb_config_setting>(builder.Configuration.GetSection("MongoAfDbSettings"));
builder.Services.AddSingleton<mongo_operation_service>();
var app = builder.Build();

//getapi call for intro page
DateTime datetime_var = DateTime.Now;
app.MapGet("/", () => $"                WELCOME TO THE 'CENTER OF SCHOOL' - THE TRAINING CENTER                                                             Time- {datetime_var}");

//get api call for fetch all center details
app.MapGet("/getapi/training_center_list", async (mongo_operation_service mongo_operation_service) =>
{
    var trainingCenters = await mongo_operation_service.GetAll();

    return Results.Ok(trainingCenters);
});




// post api call for save center details on database
app.MapPost("/postapi/add_center_details", async (mongo_operation_service mongo_operation_service, code_for_mongo_operation code_for_mongo_operation) =>
{
    ModelStateDictionary modelState = new ModelStateDictionary();
    if (!modelState.IsValid)
    {
        return Results.BadRequest(modelState);
    }

    //phone number validation
    if (!Validator.IsValid(code_for_mongo_operation.ContactPhone))
    {
        //ModelState.AddModelError("ContactPhone", "Invalid phone number format.");
        return Results.BadRequest("Contact phone is required, Please enter a correct number with 10 digit");
    }

    if (!Validator.IsValidPhoneNumber(code_for_mongo_operation.ContactPhone))
    {
        return Results.BadRequest("Contact phone is invalid, Please give a valid phone number having 10 digits");
    }

    //email validation
    if (!Validator.IsValidEmail(code_for_mongo_operation.ContactEmail))
    {
        return Results.BadRequest("Email is invalid, Please enter a valid email");
    }



    //CenterName validation => less than 40
    if (!Validator.LengthCheck(code_for_mongo_operation.CenterName))
    {
        return Results.BadRequest("Center Name character size exceeded, Please enter within 40 characters");
    }

  
    //CoursesOffered list empty check
    if (!Validator.ListSizeCheck(code_for_mongo_operation.CoursesOffered))
    {
        return Results.BadRequest("CourseOffered should not be empty, Please give at least one course name");
    }

    //centercode alphanumeric check
    if (!Validator.AlphaNumericCheck(code_for_mongo_operation.CenterCode))
    {
        return Results.BadRequest("CenterCode will be always 12 characters and alphanumeric, Please make sure atleast 1 alphabate and 1 digit is there");
    }



    await mongo_operation_service.Create(code_for_mongo_operation);
    return Results.Ok(code_for_mongo_operation);




});

app.Run();