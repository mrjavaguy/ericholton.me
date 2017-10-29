Title: Retail Planning Part 1
Published: 10/28/2017
Tags: 
  - C#
  - retail
  - planing
---
# Retail Planning Application
I have decide that just showing how I am doing the functional exercises would be a little boring. Instead I am going to build an application 
from the beginning using what I have learned from the [*Functional Programming in C#*](https://www.manning.com/books/functional-programming-in-c-sharp) book.
The application I will work on is a convertion of an Windows application into a web application, at the same time I will open source the code around it.

## Domain Description
This application is a planning system for retailers. In this case it for brick and morter stores more than for web based retilers. 
The data set is rather interesting in that there are several hierarchies of data. 

There is a date based one, this a fairly shallow typically. 

Example:

Year<br />
Season (Spring, Fall)<br />
Month<br />

The product base hierarchy can be faily deep or not depending on the retailer.

Example:

Company<br />
Division (Men's, Women's)<br />
Category<br />
Class (Pant, Tops, Accessories)<br />
Subclass (Socks, Ties, Belts)<br />

The last hierarchy is the store based, again this depends on the retailer.

Example:

Company<br />
Region<br />
District<br />
Store<br />

For MVP of this site we all be doing a minimuin set of variables for planning. Those will be

Beginning of Period Inventory<br />
Sales<br />
Last Year Sales (Actual)<br />
Markdowns<br />
Receipts<br />
Shrinkage<br />
End of Period Inventory<br />

## Some architecture decisions

For this I am going to use some library and tools that I have been wanted to learn more about. For the web api I am using ASP.NET MVC Core. For the database, I am going to use Martan with Postgres.