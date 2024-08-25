# Continues Assessment App - A C# Beginner's Project

**This project is intended to serve as a beginner level project in c#**.  

This is a simple continues assessment app for managing the marks of students scored for their registered courses

## Functions
- Add/Update Student information
- Add/Update Courses
- Add/Update Scores
- Calculate GPA

## Motivation
A quick google search on "_c# beginner project_" will return pages with a list of projects which are common accross all the pages.  

These list of projects, from my perspective have two things in common;  
1. Half of the projects on these lists, like a **Todo** app or an **Address Book** app, are very simple apps which is coded all in one file or at most across two files.
- Such projects fail to teach the beginner, how to design a non trivial application with multiple, seperate moving parts that at the same time work together to produce a complete application.  
- These apps are usually too simple. After completion, beginners find themselves stuck on how to move on to the next step, which is, designing non trivial apps.  

2. The other half of these projects are either one of a GUI app, a network app or a "simple" web app.
- These apps adds the ~~uneccessary~~ complexity of frameworks or libraries that make it difficult for the beginner to wrap their mind around the project as a whole.  

### This Project

This project tries to use mostly the basic and raw c# language features while at the same time being complex enough in other to highlight ways to structure a non trivial application using the clasic **MVC** software design pattern.  

>The purpose of this project is to help the beginner learn and appriciate how they would put together all the basic c# features and the pieces they have learnt to create a non trivial application the **right way**

## The Project Stucture - MVC and some VMs
We adopted the **M**odel **V**iew **C**ontroller pattern for this project because it is one of the most commonly used software design pattern and also, most **CRUD** (Create, Read, Update and Delete) applications, like this project and UI apps tend to fit naturally into this pattern.

Almost all the classes in this project are either one of a **Model**, **View**, **Controller**. 

There are also **~~ViewModels~~** (VM), that are just projection of data into strongly typed objects to be injected into the views. In other words, **ViewModels** hold the data that is to be displayed in the views.

>**Note** that there are no data binding, data validation or complex business logic in this our **ViewModels**. They are just there to hold *view data*

>Data validation and processing of business logic are done in the contollers.

### Models
1. `Student`
2. `Course`
3. `StudentMark`

### Views
>NOTE: This project is not GUI base. But then, we wanted to be a bit more adventurous, so we added some generic (console) UI's to help ease user interaction with the app.  

1. `TableView` - Renders a table with a list of provided `ITableItem`s.  
- The `TableView` add support for up/down arrow keys for easy navigation.  

2. `Menu` - Renders a menu with support for **up/down** arrow keys for easy navigation.

### Controllers
1. `StudentController` - Manage creating and updating `Student` information.
2. `CourseController` - Manage creating and updating `Courses`
3. `AssessmentController` - Manage registering courses for students, adding and updating student marks. 

### ViewModels (VMs)
1. `StudentMarkViewModel` - We project data to objects of this class, to be injected into a `TablewView` for displaying a **_list of students_** and their respective marks scored for a selected course.
2. `CourseMarkViewModel` - Holds projected data to be injected into a `TableView` for displaying a **_list of registered courses_** and respective marks scored for a selected student.

>**Note**: Unlike **ViewModel**s in the **MVVM** pattern, there is obviously no data binding here. The purpose for these objects are just for data projection.

## Data and Database
In other not to deviate from the motive of this project, which is, keeping things as simple and straight forward as possible, we do not use any SQL/NoSQL database engine. We simply dump the data of the app to a json file when we are **saving changes**. We read from the json file whenever the app re-starts.

>We simply meant to say; we use a json file as a database.