Title: Week Interest 2020-02-01
Published: 01/05/2020
Tags:
  - C#
  - Kubernetes
  - Istio
  - Visual Studio Code
---

# Key Reads of the Week

This week, my browser was filled with tabs that I kept revisiting, each offering valuable insights into different aspects of software development. Here’s a rundown of the articles I found most intriguing and why they caught my attention.

## Serilog and ASP.NET Core 3

[Using Serilog ASP.NET Core in ASP.NET Core 3 - Excluding Health Check Endpoints from Serilog Request Logging](https://andrewlock.net/using-serilog-aspnetcore-in-asp-net-core-3-excluding-health-check-endpoints-from-serilog-request-logging/)

In our projects, we use Serilog for logging, and integrating it with ASP.NET Core 3 has been crucial. However, our Splunk logs were getting cluttered with entries from health check calls. This article by Andrew Lock provided a clear guide on how to exclude these endpoints from Serilog’s request logging, helping us keep our logs clean and relevant.

## Automatic Unit Testing in .NET Core

[Automatic Unit Testing in .NET Core Plus Code Coverage in Visual Studio Code](https://www.hanselman.com/blog/AutomaticUnitTestingInNETCorePlusCodeCoverageInVisualStudioCode.aspx)

Scott Hanselman's blog post discusses the importance of automatic unit testing in .NET Core and how to implement code coverage in Visual Studio Code. The idea of automating these tests is appealing as it can significantly enhance development efficiency. I'm planning to integrate this into my workflow to see how it boosts productivity and ensures code quality.

## Learning Istio

[A Bit of Istio Before Tea Time](https://blog.alexellis.io/a-bit-of-istio-before-tea-time/)

As part of my ongoing learning about Kubernetes and its ecosystem, I delved into Istio, a powerful service mesh that provides a way to control how microservices share data. This article by Alex Ellis offered a casual yet informative overview of Istio, making it less daunting and more approachable.

## Designing a Microservice Framework

[Inserting Middleware Between UseRouting and UseEndpoints as a Library Author – Part 1](https://andrewlock.net/inserting-middleware-between-userouting-and-useendpoints-as-a-library-author-part-1/)

Currently, I’m in the process of designing a microservice framework for my work. Andrew Lock's insights into effectively inserting middleware in ASP.NET Core’s request pipeline have been invaluable. This is particularly pertinent as it pertains to enhancing the scalability and maintainability of our microservices.

---

These articles not only provided solutions to specific problems but also broadened my understanding of key concepts in modern software development. I'm excited to apply these learnings in my projects and will continue to share updates on my progress and new discoveries.
