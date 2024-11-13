using ex25_1_ferramentas.DAO;
using ex25_1_ferramentas.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ex25_1_ferramentas.Controllers
{
    public class FerramentasController : Controller
    {
        public IActionResult Index()
        {
            try
            {
                FerramentasDAO dao = new FerramentasDAO();
                var lista = dao.Listagem();
                return View(lista);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Create()
        {
            try
            {
                FerramentasViewModel ferramenta = new FerramentasViewModel();
                ViewBag.operacao = "I";
                return View("Form", ferramenta);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                FerramentasDAO dao = new FerramentasDAO();
                FerramentasViewModel ferramenta = dao.Consulta(id);

                ViewBag.operacao = "A";
                return View("Form", ferramenta);
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Salvar(FerramentasViewModel ferramenta, string operacao)
        {
            try
            {
                FerramentasDAO dao = new FerramentasDAO();

                ModelState.Clear();
                if (string.IsNullOrEmpty(ferramenta.Descricao))
                    ModelState.AddModelError("Descricao", "Campo obrigatório!");
                if (ferramenta.FabricanteId <= 0)
                    ModelState.AddModelError("FabricanteId", "Campo obrigatório! número tem que ser maior que 0");

                if (ModelState.IsValid)
                {
                    if (operacao == "I")
                        dao.Inserir(ferramenta);
                    else
                        dao.Alterar(ferramenta);
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.operacao = operacao;
                    return View("Form", ferramenta);
                }
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                FerramentasDAO dao = new FerramentasDAO();
                dao.Excluir(id);
                return RedirectToAction("index");
            }
            catch (Exception erro)
            {
                return View("Error", new ErrorViewModel(erro.ToString()));
            }
        }
    }
}

