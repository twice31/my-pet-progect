using AutoMapper;
using BookExchange.Application.Books.DTOs;
using BookExchange.Application.Contracts;
using Domain.Book.VO;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Book;

namespace BookExchange.Application.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto?>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto?> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var bookId = BookId.Create(request.Id);

                var book = await _bookRepository.GetByIdWithLockAsync(bookId);

                if (book == null)
                {
                    await _unitOfWork.RollbackTransactionAsync();
                    return null;
                }

                book.UpdateDetails(
                    Title.Create(request.Title),
                    Author.Create(request.Author),
                    ISBN.Create(request.ISBN)
                );

                await _unitOfWork.SaveChangesAsync();

                await _unitOfWork.CommitTransactionAsync();

                return _mapper.Map<BookDto>(book);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw;
            }
        }
    }
}