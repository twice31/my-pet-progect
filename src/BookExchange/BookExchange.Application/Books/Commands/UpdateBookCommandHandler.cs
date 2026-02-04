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
        private readonly IDbTransactionManager _transactionManager;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(
            IBookRepository bookRepository,
            IUnitOfWork unitOfWork,
            IDbTransactionManager transactionManager,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
            _transactionManager = transactionManager;
            _mapper = mapper;
        }

        public async Task<BookDto?> Handle(UpdateBookCommand request, CancellationToken ct)
        {
            await _transactionManager.BeginTransactionAsync(ct);

            try
            {
                var bookId = BookId.Create(request.Id);
                var book = await _bookRepository.GetByIdWithLockAsync(bookId, ct);

                if (book == null)
                {
                    await _transactionManager.RollbackTransactionAsync(ct);
                    return null;
                }

                book.UpdateDetails(
                    Title.Create(request.Title),
                    Author.Create(request.Author),
                    ISBN.Create(request.ISBN)
                );

                await _unitOfWork.SaveChangesAsync(ct);
                await _transactionManager.CommitTransactionAsync(ct);

                return _mapper.Map<BookDto>(book);
            }
            catch (Exception)
            {
                await _transactionManager.RollbackTransactionAsync(ct);
                throw;
            }
        }
    }
}