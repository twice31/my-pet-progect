using AutoMapper;
using BookExchange.Application.Books.DTOs;
using BookExchange.Application.Contracts;
using Domain.Book;
using Domain.Book.VO;
using Domain.User.VO; 
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var title = Title.Create(request.Title);
            var author = Author.Create(request.Author);
            var isbn = ISBN.Create(request.ISBN);

            var ownerId = UserId.Create(request.OwnerId);

            var newBook = Book.New(title, author, isbn, ownerId);

            await _bookRepository.AddAsync(newBook);
            await _bookRepository.SaveChangesAsync();

            var bookDto = _mapper.Map<BookDto>(newBook);

            return bookDto;
        }
    }
}