﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AccessManagement.Attributes;
using AccessManagement.Models;

namespace AccessManagement
{
    public class SubSystemService
    {
        public List<ModuleModel> GetAll(Assembly assembly)
        {
            var controllers = assembly
                  .GetTypes()
                  .Where(type => typeof(Controller).IsAssignableFrom(type)
                  &&
                  type
                  .CustomAttributes
                  .Any(i => i.AttributeType.Equals(typeof(HasModuleAttribute))))
                  .ToList();

            var modules = new List<ModuleModel>();

            controllers.ForEach(c =>
            {
                var module = new ModuleModel();

                module.TitleEn = c.Name.Substring(0, c.Name.IndexOf("Controller"));

                var moduleAttr = c
                   .CustomAttributes
                   .FirstOrDefault(i => i.AttributeType.Equals(typeof(HasModuleAttribute)));

                var title = moduleAttr
                .NamedArguments
                .FirstOrDefault(i => i.MemberName == "Title");

                if (title != null)
                {
                    module.Title = title.TypedValue.Value?.ToString();
                }

                var actions = c.
                GetMethods()
                .Where(i => i.CustomAttributes.Any(p => p.AttributeType.Equals(typeof(HasActionAttribute))))
                .ToList();

                actions.ForEach(i =>
                {
                    var ac = new ActionModel();

                    ac.TitleEn = i.Name;

                    var acAttribute = i.CustomAttributes.FirstOrDefault(p => p.AttributeType.Equals(typeof(HasActionAttribute)));

                    var acTitle = acAttribute
                    .NamedArguments
                   .FirstOrDefault(p => p.MemberName == "Title");

                    if (acTitle != null)
                    {
                        ac.Title = acTitle.TypedValue.Value.ToString();
                    }

                    module.Actions.Add(ac);

                });

                modules.Add(module);
            });

            return modules;
        }
    }
}
