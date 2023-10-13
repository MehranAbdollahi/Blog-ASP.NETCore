using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.CoreLayer.DTOs.Comments;
using Blog.CoreLayer.Utilities;

namespace Blog.CoreLayer.Services.Comments
{
    public interface ICommentService
    {
        OperationResult CreateComment(CreateCommentDto command);
        List<CommentDto> GetPostComments(int postId);
    }
}