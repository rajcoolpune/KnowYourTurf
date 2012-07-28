﻿using System.Linq;
using System.Web.Mvc;
using KnowYourTurf.Core;
using KnowYourTurf.Core.Domain;
using KnowYourTurf.Core.Html;
using KnowYourTurf.Core.Services;
using KnowYourTurf.Web.Models;
using KnowYourTurf.Web.Services;
using StructureMap;

namespace KnowYourTurf.Web.Controllers
{
    public class FieldController : KYTController
    {
        private readonly IRepository _repository;
        private readonly ISaveEntityService _saveEntityService;

        public FieldController(IRepository repository,
            ISaveEntityService saveEntityService)
        {
            _repository = repository;
            _saveEntityService = saveEntityService;
        }

        public ActionResult AddUpdate(ViewModel input)
        {
            var field = input.EntityId > 0 ? _repository.Find<Field>(input.EntityId) : new Field();
            var model = new FieldViewModel
            {
                Item = field,
                ParentId = input.ParentId,
                AddUpdateUrl = UrlContext.GetUrlForAction<FieldController>(x => x.AddUpdate(null)) + "/" + field.EntityId,
                _Title = WebLocalizationKeys.FIELD_INFORMATION.ToString()
            };
            return PartialView("FieldAddUpdate", model);
        }

        public ActionResult Delete(ViewModel input)
        {
            var field = _repository.Find<Field>(input.EntityId);
            var rulesEngineBase = ObjectFactory.Container.GetInstance<RulesEngineBase>("DeleteFieldRules");
            var rulesResult = rulesEngineBase.ExecuteRules(field);
            if (!rulesResult.Success)
            {
                Notification notification = new Notification(rulesResult);
                return Json(notification);
            } 
            _repository.SoftDelete(field);
            _repository.UnitOfWork.Commit();
            return null;
        }

        public ActionResult Save(FieldViewModel input)
        {
            var category = _repository.Find<Category>(input.ParentId);
            Field field;
            if (input.Item.EntityId > 0) { field = category.Fields.FirstOrDefault(x => x.EntityId == input.Item.EntityId); }
            else
            {
                field = new Field();
                category.AddField(field);
            }
            field.Description = input.Item.Description;
            field.Name = input.Item.Name;
            field.Abbreviation= input.Item.Abbreviation;
            field.Size = input.Item.Size;
            field.Status = input.Item.Status;
            field.FieldColor= input.Item.FieldColor;
            
            var crudManager = _saveEntityService.ProcessSave(category);
            var notification = crudManager.Finish();
            return Json(notification,"text/plain");
        }

    }
}