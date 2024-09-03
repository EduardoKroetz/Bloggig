import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { FormsModule } from '@angular/forms';
import Post from '../../interfaces/Post';
import { PostService } from '../../services/post.service';
import { ProfilePostComponent } from "../profile-post/profile-post.component";

@Component({
  selector: 'app-search-post',
  standalone: true,
  imports: [CommonModule, FormsModule, ProfilePostComponent],
  templateUrl: './search-post.component.html',
  styleUrl: './search-post.component.css'
})
export class SearchPostComponent {
  @Input() userId = ""
  input : string = ""
  posts : Post[] | null = null
  loadingPosts = true;
  filteredPosts : Post[] | undefined | null = [];

  constructor(private postService: PostService) {}

  ngOnInit(): void {
    this.postService.getUserPosts(this.userId, 1000, 1).subscribe(
      (res: any) => {
        this.posts = res.data;
        this.filteredPosts = this.posts;
        this.loadingPosts = false
      },
      (error) => {
        this.loadingPosts = false
      }
    );
  }

  handleSearchPosts(event: SubmitEvent) {
    event.preventDefault();

    this.filterPosts();
  }

  private filterPosts() {
    this.filteredPosts = this.posts?.filter(post => 
      post.content.toLowerCase().includes(this.input.toLowerCase()) || 
      post.title.toLowerCase().includes(this.input.toLowerCase()) || 
      post.tags.some(tag => tag.name.toLowerCase().includes(this.input.toLowerCase()))
    )
  }
}
