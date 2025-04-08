//using CrossCutting.Enums;
//using Domain.Entities;
//using Dto.Address;
//using Dto.OrderItems.Requests;
//using Dto.Orders.Requests;
//using Dto.Resales.Requests;
//using Orders.Worker.Shared.Requests;

//namespace Solution.Tests.Init
//{
//    public static class OrdersInit
//    {
//        public static OrderRequestDto CreateDto()
//        {
//            return new OrderRequestDto
//            {
//                ResaleId = "resale-123",
//                Items = new List<OrderItemsRequestDto>
//                {
//                    new OrderItemsRequestDto { Name = "prod-1", Quantity = 2, Price = 100 },
//                    new OrderItemsRequestDto { Name = "prod-2", Quantity = 1, Price = 200 }
//                },
//                Resale = new ResalesRequestDto()
//                {
//                    Address = new AddressDto()
//                    {
//                        Number = "456666900",
//                        ZipCode = "99999999"
//                    },
//                    Cnpj = "17373221000108",
//                }
//            };
//        }

//        public static List<Order> CreateOrderQuantityLessThan1000()
//        {
//            return new List<Order>
//            {
//                new Order
//                {
//                    Id = "order-1",
//                    Resale = new Resale { Id = "resale-123" },
//                    Status = OrderStatus.Received,
//                    Items = new List<OrderItems>
//                    {
//                        new() { Name = "Item A", Quantity = 200, Price = 10 },
//                        new() { Name = "Item A", Quantity = 500, Price = 10 }
//                    }
//                }
//            };
//        }

//        public static List<Order> CreateOrder()
//        {
//            return new List<Order>
//            {
//                new Order
//                {
//                    Id = "order-1",
//                    Resale = new Resale { Id = "resale-123" },
//                    Status = OrderStatus.Received,
//                    Items = new List<OrderItems>
//                    {
//                        new() { Name = "Item A", Quantity = 600, Price = 10 },
//                        new() { Name = "Item A", Quantity = 500, Price = 10 }
//                    }
//                }
//            };
//        }
//    }
//}
