package com.example.domain.use_case

import com.example.domain.dto.BooksData
import com.example.domain.dto.TokenData

interface BackUseCase {
    suspend fun login(username: String, password: String): TokenData?
    suspend fun getBooks(token: String): List<BooksData>?
//    suspend fun addBook(token: String): BooksData?
    suspend fun deleteBook(token: String, id: String)
}