﻿using EAappProject.Controls;
using EAappProject.Driver;
using EAappProject.Model;
using Microsoft.Playwright;

namespace EAappProject.Pages
{
    public interface ICreateProductPage
    {
        Task<CreateProductPage> ValidateTitleAsync();
        Task CreateProductAsync(ProductDetails productDetails);
    }

    public class CreateProductPage(IPage page) : ICreateProductPage
    {
        ILocator pageTitleTxt => page.GetByRole(AriaRole.Heading, new() { Name = "Create" });
        ILocator txtName => page.GetByRole(AriaRole.Textbox, new() { Name = "Name" });
        ILocator txtDescription => page.GetByRole(AriaRole.Textbox, new() { Name = "Description" });
        ILocator txtPrice => page.Locator("#Price");
        ILocator txtProductType => page.GetByLabel("ProductType");
        ILocator btnCreate => page.GetByRole(AriaRole.Button, new() { Name = "Create" });

        public async Task<CreateProductPage> ValidateTitleAsync()
        {
            await pageTitleTxt.IsVisibleAsync();
            return this;
        }
        public async Task CreateProductAsync(ProductDetails productDetails)
        {
            await txtName.ClearAndFillElementAsync(productDetails.Name);
            await txtDescription.ClearAndFillElementAsync(productDetails.Description);
            await txtPrice.ClearAndFillElementAsync(productDetails.Price.ToString());
            await txtProductType.SelectDropDownWithValueAsync(productDetails.ProductType.ToString());
            await btnCreate.ClickAsync();
        }
    }
}
