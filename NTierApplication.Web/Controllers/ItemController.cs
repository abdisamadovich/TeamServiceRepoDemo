using Microsoft.AspNetCore.Mvc;
using NTierApplication.Service;
using NTierApplication.Service.ViewModels;
using Swashbuckle.AspNetCore.Annotations;

namespace NTierApplication.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService ItemService;

        public ItemController(IItemService itemService)
        {
            ItemService = itemService;
        }
        //fork qilishni urganyapman
        [HttpGet]
        [Route("")]
        [SwaggerOperation(OperationId = "GetAll")]
        public ICollection<ItemViewModel> GetAll()
        {
            return ItemService.GetItems();
        }

        [HttpPost(Name = "CreateNew")]
        public ItemViewModel CreateNew(ItemViewModel itemViewModel)
        {
            ItemService.CreateNew(itemViewModel);
            return itemViewModel;
        }

        [HttpGet]
        [Route("{id}")]
        [SwaggerOperation(OperationId = "GetById")]
        public ItemViewModel GetById(long id)
        {
            return ItemService.GetById(id);
        }

        [HttpPut]
        public ItemViewModel Update(ItemViewModel itemViewModel)
        {
            ItemService.Update(itemViewModel);
            return itemViewModel;
        }

        [HttpDelete]
        [Route("{id}")]
        public void Delete(long id)
        {
            ItemService.Delete(id);
        }
    }
}
