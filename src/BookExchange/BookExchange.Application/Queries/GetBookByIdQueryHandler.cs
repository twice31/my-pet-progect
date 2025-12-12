using AutoMapper;
using BookExchange.Application.Contracts;
using BookExchange.Application.Books.DTOs;
using Domain.Book.VO;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto?> Handle(GetBookByIdQuery request, CancellationToken ct)
        {
            var bookId = BookId.Create(request.BookId);
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
            {
                return null;
            }

            return _mapper.Map<BookDto>(book);
        }
    }
}