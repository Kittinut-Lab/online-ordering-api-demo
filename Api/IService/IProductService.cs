﻿using System;
using System.Threading.Tasks;
using online_ordering_api.Models;

namespace online_ordering_api.IService
{
    public interface IProductService
    {
        ListProductsResponseEntity ListProductService();
    }
}
