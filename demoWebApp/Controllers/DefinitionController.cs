using demoWebApp.Models.ViewBinding;
using MagenicMetrics;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace demoWebApp.Controllers
{
    public class DefinitionController : BaseController
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="DefinitionController" /> class.
        /// </summary>
        /// <param name="metric">The metric.</param>
        public DefinitionController(IMetric metric) : base(metric)
        {
            if (definitions == null)
            {
                definitions = new List<DefinitionIndexView>() { new DefinitionIndexView { DefinitionId = 1, IsDeleted = false, Name = "Definition 1" } };
            }
        }

        private static List<DefinitionIndexView> definitions;

        // GET: Definition/Create
        public ActionResult Create() => View(new DefinitionIndexView());

        // POST: Definition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DefinitionIndexView newDefinition)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(newDefinition);
                }
                var nextId = definitions.Max(d => d.DefinitionId) + 1;
                newDefinition.DefinitionId = nextId;
                definitions.Add(newDefinition);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return View(newDefinition);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var itemToDelete = definitions.FirstOrDefault(d => id == d.DefinitionId);
                if (itemToDelete == null)
                {
                    return NotFound();
                }
                itemToDelete.IsDeleted = true;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                return BadRequest(genEx.Message);
            }
        }

        // GET: Definition/Details/5
        public ActionResult Details(int id)
        {
            var itemToView = definitions.FirstOrDefault(d => id == d.DefinitionId);
            return itemToView == null ? NotFound() : (ActionResult)View(itemToView);
        }

        public ActionResult Edit(int id) => View(definitions.Find(d => d.DefinitionId == id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DefinitionIndexView definition)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(definition);
                }
                var editedItem = definitions.Find(d => d.DefinitionId == definition.DefinitionId);
                editedItem.Name = definition.Name;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return View();
            }
        }

        // GET: Definition
        public ActionResult Index() => View(definitions);

        public IActionResult Undelete(int id)
        {
            try
            {
                var itemToUndelete = definitions.FirstOrDefault(d => id == d.DefinitionId);
                if (itemToUndelete == null)
                {
                    return NotFound();
                }
                if (!itemToUndelete.IsDeleted)
                {
                    return BadRequest("Item not deleted");
                }
                itemToUndelete.IsDeleted = false;
                return RedirectToAction(nameof(Index));
            }
            catch (Exception genEx)
            {
                ModelState.AddModelError("", genEx.Message);
                return BadRequest(genEx.Message);
            }
        }
    }
}
