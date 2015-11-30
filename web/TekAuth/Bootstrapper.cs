﻿using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using AutoMapper;
using StructureMap;
using Tekconf.Data.Entities;

namespace TekAuth
{
    public class Bootstrapper
    {
        public void Init()
        {
            Mapper.CreateMap<Conference, Tekconf.DTO.Conference>();
        }
    }

    public class StructureMapWebApiControllerActivator : IHttpControllerActivator
    {
        private readonly IContainer _container;

        public StructureMapWebApiControllerActivator(IContainer container)
        {
            _container = container;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var nested = _container.GetNestedContainer();
            var instance = nested.GetInstance(controllerType) as IHttpController;
            request.RegisterForDispose(nested);
            return instance;
        }
    }
}