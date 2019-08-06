using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using AccessManagement.Attributes;
using AccessManagement.Models;
using AccessManagement.UI.DataLayer.Entities;

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

                    var affiliateAttribute = i.CustomAttributes.FirstOrDefault(p => p.AttributeType.Equals(typeof(HasAffiliateAttribute)));

                    if (affiliateAttribute != null)
                    {
                        var affiliate = affiliateAttribute
                    .NamedArguments
                   .FirstOrDefault(p => p.MemberName == "AffiliateName");

                        if (affiliate != null)
                        {
                            ac.AffiliateName = affiliate.TypedValue.Value?.ToString();
                        }
                    }

                    module.Actions.Add(ac);

                });

                modules.Add(module);
            });

            return modules;
        }

        public List<ModuleModel> GetAllWithPermision(Assembly assembly, List<UserAccess> userAccess)
        {
            var modules = new SubSystemService()
               .GetAll(Assembly.GetExecutingAssembly());

            userAccess.GroupBy(c => c.ControllerName).ToList().ForEach(c =>
            {
                var module = modules.FirstOrDefault(i => i.TitleEn == c.Key);

                if (module != null)
                {
                    module.HasPermision = true;

                    foreach (var role in c)
                    {
                        var ac = module.Actions.FirstOrDefault(p => p.TitleEn == role.ActionName);

                        if (ac != null)
                        {
                            ac.HasPermision = true;
                        }
                    }
                }

            });

            return modules;
        }
    }
}
