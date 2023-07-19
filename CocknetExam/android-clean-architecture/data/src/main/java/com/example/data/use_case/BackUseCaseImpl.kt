package com.example.data.use_case

import com.example.data.data_source.DataSources
import com.example.domain.dto.BooksData
import com.example.domain.use_case.BackUseCase
import retrofit2.awaitResponse
import kotlin.random.Random.Default.nextInt

class BackUseCaseImpl : BackUseCase {
    override suspend fun login(username: String, password: String) =
        DataSources.bookService.login(username, password).awaitResponse().run {
            if (isSuccessful) body() else null
        }

    override suspend fun getBooks(token: String): List<BooksData>? {
        val book = BooksData("234", token, "XDD")

        return DataSources.bookService.getBooks(token).awaitResponse().run {
            if (isSuccessful) body() else listOf(book)
        }
    }

//    override suspend fun addBook(token: String): BooksData? {
//        val book = BooksData(
//            nextInt(1, 10).toString(),
//            nextInt(1, 10).toString(),
//            nextInt(1, 10).toString()
//        )
//
//        DataSources.bookService.addBook(token, book).awaitResponse().run {
//            if (isSuccessful) return body() else return null
//        }
//    }

    override suspend fun deleteBook(token: String, id: String) {
        DataSources.bookService.deleteBook(token, id).run { }
    }
}