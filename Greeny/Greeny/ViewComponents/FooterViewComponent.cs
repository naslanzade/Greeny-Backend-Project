﻿using Greeny.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Greeny.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {

        private readonly ILayoutService _layoutServie;

        public FooterViewComponent(ILayoutService layoutService)
        {
            _layoutServie = layoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var datas = await _layoutServie.GetAllDatas();
            return await Task.FromResult((IViewComponentResult)View(datas));
        }

    }
}
