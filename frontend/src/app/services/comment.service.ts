import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CommentService {

  constructor(private httpClient: HttpClient) { }

  getPostComments(postId: string, pageSize: number, pageNumber: number) {
    return this.httpClient.get(`${environment.apiUrl}/comments/posts/${postId}?pageSize=${pageSize}&pageNumber=${pageNumber}`)
  }

  createComment(postId: string, content: string) {
    return this.httpClient.post(`${environment.apiUrl}/comments`, { postId, content }, { withCredentials: true })
  }

  deleteComment(commentId: string) {
    return this.httpClient.delete(`${environment.apiUrl}/comments/${commentId}`, { withCredentials: true })
  }

  updateComment(commentId: string, content: string) {
    return this.httpClient.put(`${environment.apiUrl}/comments/${commentId}`, { content } , { withCredentials: true })
  }
}
