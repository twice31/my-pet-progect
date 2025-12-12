using AutoMapper;
using BookExchange.Application.Books.DTOs;
using BookExchange.Application.Contracts;
using Domain.Book.VO;
using MediatR;
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
            var bookId = BookId.Create(request.Id);
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
            {
                return null; // Книга не найдена
            }

            // Обновление доменной сущности с помощью доменных правил
            book.UpdateDetails(
                Title.Create(request.Title),
                Author.Create(request.Author),
                ISBN.Create(request.ISBN)
            );

            // Сохраняем изменения
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }
    }
}