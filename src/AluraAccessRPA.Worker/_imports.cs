﻿global using NLog.Extensions.Logging;
global using Quartz;
global using Microsoft.AspNetCore.Hosting;
global using MediatR;
global using LogLevel = Microsoft.Extensions.Logging.LogLevel;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using System.Reflection;
global using AluraAccessRPA.Application.Config;
global using AluraAccessRPA.Worker.Extensions;
global using AluraAccessRPA.Worker.Jobs;
global using AluraAccessRPA.Application.Selenium;
global using AluraAccessRPA.Domain.Enum;
global using AluraAccessRPA.Domain.Interfaces;
global using AluraAccessRPA.Infrastructure.Data;
global using AluraAccessRPA.Worker.Common;
global using OpenQA.Selenium;
global using OpenQA.Selenium.Chrome;