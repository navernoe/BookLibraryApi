# BookLibraryApi
RestAPI to work with books and authors

#### Books:
 
|       Operation         |  Method  |                URL                     |                PARAMS                 |
|:------------------------|:--------:|:---------------------------------------|:--------------------------------------|
|   Add book              |  POST    | `/books`                               | `{ "bookName": string }`              |
|   Remove book           |  DELETE  | `/books/remove/{id}`                   | -                                     |
|   Update book           |  POST    | `/books/update`                        | `{ "bookId": number, "name": string}` |
|   Get book by id        |  GET     | `/books`                               | `{ "id": number }`                    |
|   Get all books         |  GET     | `/books/all`                           | -                                     |

#### Authors:
|       Operation         |  Method  |                URL                     |                 PARAMS                  |
|:------------------------|:--------:|:---------------------------------------|:----------------------------------------|
|   Add author            |  POST    | `/authors`                             | `{ "authorName": string }`              |
|   Update author         |  POST  | `/authors/update`                        | `{ "authorId": number, "name": string}` |
|   Remove author         |  DELETE  | `/authors/remove/{id}`                 | -                                       |
|   Get author by id      |  GET     | `/authors`                             | `{ "id": number }`                      |
|   Get all authors       |  GET     | `/authors/all`                         | -                                       |

#### Authors' and books' links
|              Operation             |  Method  |            URL            |                  PARAMS                   |
|:-----------------------------------|:--------:|:--------------------------|:------------------------------------------|
|   Add link                         |  POST    | `/library`                | `{ "authorId": number, "bookId": number }`|
|   Remove link                      |  DELETE  | `/library/remove`         | `{ "authorId": number, "bookId": number }`|
|   Get books count by author id     |  GET     | `/library`                | `{ "authorId": number }`                  |

## Task Description

> Разработать:  
> 
> 1. Структуру базы данных для хранение данных об авторах и книгах. Каждый автор может написать несколько книг и у каждой книги может быть несколько авторов.
> 2. Приложение на ASP.NET Core, которое предоставляет REST API:
>     a.     для добавления, редактирование и удаление данных из БД;
>     b.     для отображения количества книг определенного автора.
> 
> В качестве СУБД желательно использовать PostgreSQL.