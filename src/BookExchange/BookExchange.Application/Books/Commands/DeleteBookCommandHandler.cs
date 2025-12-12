using BookExchange.Application.Contracts;
using Domain.Book.VO;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BookExchange.Application.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken ct)
        {
            var bookId = BookId.Create(request.Id);
            var book = await _bookRepository.GetByIdAsync(bookId);

            if (book == null)
            {
                return false; // Книга не найдена
            }

            // Удаляем сущность через репозиторий
            _bookRepository.Delete(book);

            // Сохраняем изменения
            await _unitOfWork.SaveChangesAsync();

            return true;
        }
    }
}