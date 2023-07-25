﻿using Amazon.DTO;
using AmazonAdmin.Domain;
using AmazonAdmin.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonAdmin.Application.Mapper
{
    public class AutoMapper:Profile
    {
        public AutoMapper()
        {
            CreateMap<Category, SubCategoryDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category,arCategoryDTO>().ReverseMap();
            CreateMap<Category, arsubcategory>().ReverseMap();
            CreateMap<Product, ShowProductDTO>().ReverseMap();
            CreateMap<Product,AddUpdateProductDTO>().ReverseMap();
            CreateMap<Product, ArShowproduct>().ReverseMap();
            CreateMap<ApplicationUser,UserRegisterDTO>().ReverseMap();
            CreateMap<ApplicationUser,UserLoginDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemShow>().ReverseMap();
            CreateMap<Image, ImageDTO>().ReverseMap();
            CreateMap<AddUpdateProductDTO, ShowProductDTO>().ReverseMap();
			CreateMap<Task<List<Image>>, List<ImageDTO>>();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Rating, RatingDTO>().ReverseMap();
        }
    }
}
