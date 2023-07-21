﻿using MediatR;

namespace ETicaretAPI.Application.Features.Queries.AppUser.GetAllUser
{
    public class GetAllUsersQueryRequest: IRequest<GetAllUsersQueryResponse>
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}