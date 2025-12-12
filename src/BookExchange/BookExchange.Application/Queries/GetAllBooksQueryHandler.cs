using AutoMapper;
using BookExchange.Application.Contracts;
using BookExchange.Application.Books.DTOs;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Books.Queries
{
    public class GetAllBooksQueryHandler : IRequestHandler<GetAllBooksQuery, List<BookDto>>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetAllBooksQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<List<BookDto>> Handle(GetAllBooksQuery query, CancellationToken ct)
        {
            //Получаем доменные сущности
            var books = await _bookRepository.GetAllAsync();

            //Маппим в DTO
            return _mapper.Map<List<BookDto>>(books);
        }
    }
}