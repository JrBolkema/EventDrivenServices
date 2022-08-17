db = db.getSiblingDB("courses_db");
db.createCollection("courses");

const courses = [
  {
    _id: "course-a-sm",
    title: "Angular State Management",
    description:
      "While a beginning-level Angular developer entering this training would have the basic skills to create simple Angular applications, when working in a team of developers, having consistent patterns for making data available to components, modeling interactions in a observable, traceable way, and dealing with API calls is imperative.",
    numberOfDays: 3,
  },
  {
    _id: "course-a-dt",
    title: "Angular Developer Testing",
    description:
      "In a DevOps environment, developers take responsibility for the quality (internal and external) of their applications and their build and test pipelines. This course will emphasize the process for creating automated tests for our Angular applications, including low-level Unit Tests, and Isolated Integration Tests.",
    numberOfDays: 3,
  },
  {
    _id: "course-a-aa",
    title: "Advanced Angular Development",
    description:
      "This course is designed to round-off an Angular developer's education by providing advanced techniques for managing state, writing complex selector functions, understanding the rxjs library and common operators. We will also learn advanced form validation including asynchronous validation with server side approval, as well as lazy-loading Angular modules. Building feature module libraries using NGRX Component State will also be presented.",
    numberOfDays: 3,
  },
  {
    _id: "course-s-sd",
    title: "Beginning .NET Services Development",
    description:
      "This three-day course will prepare developers to deliver code in a DevOps organization.",
    numberOfDays: 3,
  },
  {
    _id: "course-s-wa",
    title: "Web APIs with .NET",
    description:
      "This three-day, instructor-led course will prepare students to design, develop, and deliver HTTP-based APIs using Microsoft's ASP.NET MVC.",
    numberOfDays: 3,
  },
  {
    _id: "course-s-dt",
    title: "Services Developer Testing",
    description:
      "In a DevOps environment, developers take responsibility for the quality (internal and external) of their applications and their build and test pipelines. This course will emphasize the process for creating automated tests for our services, including low-level Unit Tests, and Isolated Integration Tests.",
    numberOfDays: 3,
  },
  {
    _id: "course-s-md",
    title: "Microservices Development",
    description:
      "This three-day instructor-led course will explore integration and deployment patterns for Microservice applications.",
    numberOfDays: 3,
  },
  {
    _id: "course-s-ed",
    title: "Building Event Driven Services",
    description:
      "This three-day instructor led class will prepare students to plan, develop, and deploy Event Driven service architectures.",
    numberOfDays: 3,
  },
];
db.courses.remove({});
db.courses.insertMany(courses);

function getDateInFuture(days) {
  let today = new Date();
  today = new Date(today.setHours(0, 0, 0, 0));
  return new Date(today.setDate(today.getDate() + days)).toISOString();
}
const offerings = [
  {
    _id: "62c2ec941affac574888b80e",
    revision: 1,
    course: "course-s-md",
    startDate: getDateInFuture(10),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2ecf61affac574888b810",
    revision: 1,
    course: "course-a-sm",
    startDate: getDateInFuture(20),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2f2801affac574888b812",
    revision: 1,
    course: "course-a-sm",
    startDate: getDateInFuture(30),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2f2b01affac574888b814",
    revision: 1,
    course: "course-a-dt",
    startDate: getDateInFuture(40),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2f2df1affac574888b816",
    revision: 1,
    course: "course-a-aa",
    startDate: getDateInFuture(50),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2f3071affac574888b818",
    revision: 1,
    course: "course-s-sd",
    startDate: getDateInFuture(60),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
  {
    _id: "62c2f32b1affac574888b81a",
    revision: 1,
    course: "course-s-wa",
    startDate: getDateInFuture(70),
    startTime: "9:30 AM ET",
    endTime: "5:00 PM ET",
    location: "Online - Microsoft Teams",
    price: 323.28,
    deliveryMethod: "Online",
    hasSeatsAvailable: true,
  },
];

db = db.getSiblingDB("offerings_db");
db.createCollection("offerings");

db.offerings.remove({});
db.offerings.insertMany(offerings);

db = db.getSiblingDB("webpresence");
