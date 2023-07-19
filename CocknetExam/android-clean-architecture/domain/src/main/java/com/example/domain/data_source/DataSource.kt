package com.example.domain.data_source

import com.example.domain.dto.BooksData
import com.example.domain.dto.TokenData
import retrofit2.Call
import retrofit2.http.*

interface DataSource {
    @FormUrlEncoded
    @POST("connect/token")
    fun login(
        @Field("username") username: String,
        @Field("password") password: String,
        @Field("grant_type") grantType: String = "password"
    ): Call<TokenData>

    @GET("books/{id}")
    fun getBookDetails(@Header("Authorization") authHeader: String, @Path("id") id: String): Call<BooksData>

    @GET("books")
    fun getBooks(@Header("Authorization") authHeader: String): Call<List<BooksData>>

    @POST("books")
    fun addBook(@Header("Authorization") authHeader: String, @Body body: BooksData): Call<BooksData>

    @DELETE("books")
    fun deleteBook(@Header("Authorization") authHeader: String, @Path("id") id: String)
}