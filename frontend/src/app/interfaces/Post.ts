import Tag from "./Tag";
import User from "./User";

export default interface Post
{
  id: string,
  title: string,
  content: string,
  authorId: string,
  author: User,
  thumbnailUrl: string,
  createdAt: Date,
  updatedAt: Date,
  status: string,
  tags: Tag[]
} 