﻿using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.SignalR.HubServices
{
    public class ProductHubService : IProductHubService
    {
        public IHubContext<ProductHub> hubContext;

        public ProductHubService(IHubContext<ProductHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task ProductAddedMessageAsync(string message)
        {
            await this.hubContext.Clients.All.SendAsync(ReceiveFunctionName.ProductAddedMessage, message);

        }
    }
}
