# Nultien - The Shop

## Description

This is a code refactor tech challenge created by the Nultien company. The idea is to refactor original code and to create a service that has **good test coverage** and **error handling**, also in the refactored service we are **tracking metrics** so we have information on how many articles searches failed, how many orders failed, etc. There are **manually created indexes** for articles and inventories (we are using in-memory database) so in the situation when we high amount of data, searching/filtering will be more efficient than classic list filtering.

## Tech stack

- [.NET 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) - Console application

## Dependencies

- [Serilog](https://serilog.net/) is used for logs, console and file. Logs stored in files you can find in folder Logs/logsYYYYMMDD.txt
- [Moq](https://github.com/autofac/Autofac.Extras.Moq/releases/tag/v6.0.0) is used for mocking.

## Start

### Visual Studio 2019
- In Microsoft Visual Studio 2019 set Nultien.TheShop.Application as a startup project and press F5 on your keyboard.

### Console
- _cd_ .\src\Application\Nultien.TheShop.Application\
- _dotnet restore_
- _dotnet run_

## Tests

- Unit tests
- Integration tests

## Future improvements

This project can be easily converted into a Web Api project if we want to use it as a API or into a Background Service project if we are planing to use message queues for processing. This service can be easily deployed as a Docker container.
