import User from "./User"

export default interface Comment
{
  id: string,
  content: string,
  postId: string,
  authorId: string,
  author: User
  createdAt: string
}