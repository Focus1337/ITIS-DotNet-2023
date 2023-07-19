package com.example.domain.datasource

import com.example.domain.dto.UserData
import retrofit2.Call
import retrofit2.http.GET
import retrofit2.http.Path

interface IUserDataSource {
    @GET("users/{id}")
    fun getUserData(@Path("id") id: Int): Call<UserData>
}