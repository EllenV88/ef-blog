using Microsoft.EntityFrameworkCore;

DB db = new();
db.Users.Add(new User() { Name = "Mr. Science Guy", Password = "1234" });//0
db.Users.Add(new User() { Name = "Professor McGonagall", Password = "4321" }); //1
db.Users.Add(new User() { Name = "Lady Gogo", Password = "1342" }); //2
db.SaveChanges();

db.Blogs.Add(new Blog() { BlogName = "Science Expert" }); //0
db.Blogs.Add(new Blog() { BlogName = "Pedagogy Today" }); //1
db.Blogs.Add(new Blog() { BlogName = "Entertainment Daily" }); //2
db.SaveChanges();

//Users - articles | article -user

db.Users.First().Articles.Add(new Article { Title = "Extreme Weather" , Blog = db.Blogs.First() });
db.Users.First().Articles.Add(new Article { Title = "Latest research from the Science department", Blog = db.Blogs.First() });
db.Users.First().Articles.Add(new Article { Title = "New species discovered", Blog = db.Blogs.First() });

db.Users.Skip(1).First().Articles.Add(new Article { Title = "No mobile phones in the classroom", Blog = db.Blogs.Skip(1).First() });
db.Users.Skip(1).First().Articles.Add(new Article { Title = "New science curriculum", Blog = db.Blogs.Skip(1).First() });

db.Users.Skip(2).First().Articles.Add(new Article { Title = "Best performance so far", Blog = db.Blogs.Skip(2).First() });

db.SaveChanges();


//Articles - Categories | Category - Article

Article? article = db.Users.Where(u => u.Name == "Mr. Science Guy")?.First().Articles.Where(a => a.Title == "Extreme Weather")?.First();
article?.AddCategory(new Category { CategoryName = "Science" });

Article? article2 = db.Users.Where(u => u.Name == "Mr. Science Guy")?.First().Articles.Where(a => a.Title == "Latest research from the Science department")?.First();
article2?.AddCategory(new Category { CategoryName = "Science" });
article2?.AddCategory(new Category { CategoryName = "School" });

Article? article3 = db.Users.Where(u => u.Name == "Mr. Science Guy")?.First().Articles.Where(a => a.Title == "New species discovered")?.First();
article3?.AddCategory(new Category { CategoryName = "Science" });

Article? article4 = db.Users.Where(u => u.Name == "Professor McGonagall")?.First().Articles.Where(a => a.Title == "No mobile phones in the classroom")?.First();
article4?.AddCategory(new Category { CategoryName = "School" });

Article? article5 = db.Users.Where(u => u.Name == "Professor McGonagall")?.First().Articles.Where(a => a.Title == "New science curriculum")?.First();
article5?.AddCategory(new Category { CategoryName = "School" });
article5?.AddCategory(new Category { CategoryName = "Science" });

Article? article6 = db.Users.Where(u => u.Name == "Lady Gogo")?.First().Articles.Where(a => a.Title == "Best performance so far")?.First();
article6?.AddCategory(new Category { CategoryName = "Entertainment" });
db.SaveChanges();

Console.WriteLine("\u001b[1mUSERS:\u001b[0m");
foreach (var User in db.Users)
{
    Console.WriteLine();
    Console.WriteLine($"    \u001b[1mUserID:{User.UserId}\u001b[0m");
    Console.WriteLine($"        Username: {User.Name}");
    Console.WriteLine($"        Password: {User.Password}");
    Console.WriteLine($"        Articles:");
    foreach (Article Articles in User.Articles)
    {
        Console.WriteLine($"            - {Articles.Title}");
    }
}
    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("\u001b[1mBLOGS:\u001b[0m");
    foreach (var Blog in db.Blogs)
    {
        Console.WriteLine();
        Console.WriteLine($"    \u001b[1mBlogID:{Blog.BlogId}\u001b[0m");
        Console.WriteLine($"        Name: {Blog.BlogName}");
        Console.WriteLine($"        Articles:");
        foreach (Article Articles in Blog.Articles)
        {
            Console.WriteLine($"            - {Articles.Title}");
        }
    }

    Console.WriteLine();
    Console.WriteLine();
    Console.WriteLine("\u001b[1mArticles:\u001b[0m");
    Console.WriteLine();

    foreach (var Article in db.Users.SelectMany(u => u.Articles))
    {
        Console.WriteLine("     \u001b[1mPostID:" + Article.ArticleId + "\u001b[0m");
        Console.WriteLine($"         Title: {Article.Title}");
        Console.WriteLine($"         By {Article?.User?.Name}");
        Console.WriteLine($"         In {Article?.Blog?.BlogName}");
    Console.WriteLine($"             Categories:");
        foreach (Category Categories in Article?.Categories!)
        {
            Console.WriteLine($"            - {Categories.CategoryName}");
        }
        
        Console.WriteLine();
    }

class DB : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    string _path = "blogging.db";


    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={_path}");

}


   
/*
 
 //Users - articles | article -user

Users[0].Articles.Add(Articles[0]);
Users[0].Articles[0].User = Users[0];
Users[0].Articles.Add(Articles[1]);
Users[0].Articles[1].User = Users[0];
Users[0].Articles.Add(Articles[2]);
Users[0].Articles[2].User = Users[0];

Users[1].Articles.Add(Articles[3]);
Users[1].Articles[0].User = Users[1];
Users[1].Articles.Add(Articles[4]);
Users[1].Articles[1].User = Users[1];

Users[2].Articles.Add(Articles[5]);
Users[2].Articles[0].User = Users[2];

//Articles - Categories | Category - Article

Articles[0].Categories.Add(Categories[0]);
Articles[0].Categories[0].Articles.Add(Articles[0]);

Articles[1].Categories.Add(Categories[0]);
Articles[1].Categories[0].Articles.Add(Articles[1]);
Articles[1].Categories.Add(Categories[1]);
Articles[1].Categories[1].Articles.Add(Articles[1]);

Articles[2].Categories.Add(Categories[0]);
Articles[2].Categories[0].Articles.Add(Articles[2]);

Articles[3].Categories.Add(Categories[1]);
Articles[3].Categories[0].Articles.Add(Articles[3]);

Articles[4].Categories.Add(Categories[0]);
Articles[4].Categories[0].Articles.Add(Articles[4]);
Articles[4].Categories.Add(Categories[1]);
Articles[4].Categories[1].Articles.Add(Articles[4]);

Articles[5].Categories.Add(Categories[2]);
Articles[5].Categories[0].Articles.Add(Articles[5]);

// Blog - Article | Article - Blog

Blogs[0].Articles.Add(Articles[0]);
Blogs[0].Articles[0].Blog = Blogs[0];
Blogs[0].Articles.Add(Articles[1]);
Blogs[0].Articles[1].Blog = Blogs[0];
Blogs[0].Articles.Add(Articles[2]);
Blogs[0].Articles[2].Blog = Blogs[0];

Blogs[1].Articles.Add(Articles[3]);
Blogs[1].Articles[0].Blog = Blogs[1];
Blogs[1].Articles.Add(Articles[4]);
Blogs[1].Articles[1].Blog = Blogs[1];

Blogs[2].Articles.Add(Articles[5]);
Blogs[2].Articles[0].Blog = Blogs[2];


*/
