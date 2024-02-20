using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    public class User
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public List<Article> Articles { get; } = new();

        public void AddArticle(Article a)
        {
            Articles.Add(a);
            a.User = this;
        }
    }

    public class Article
    {
        public int ArticleId { get; set; }
        public string? Title { get; set; }
        public int BlogId { get; set; }
        public Blog? Blog { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }

        public List<Category> Categories { get; } = new();
        public void AddCategory(Category c)
        {
            Categories.Add(c);
            c.Articles.Add(this);
        }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public List<Article> Articles { get; } = new();

        public void AddArticle(Article a)
        {
            Articles.Add(a);
            a.Categories.Add(this);
        }
    }

    public class Blog
    {
        public int BlogId { get; set; }

        public string? BlogName { get; set; }

        public List<Article> Articles { get; } = new();

        public void AddArticle(Article a)
        {
            Articles.Add(a);
            a.Blog = this;

        }
    }

