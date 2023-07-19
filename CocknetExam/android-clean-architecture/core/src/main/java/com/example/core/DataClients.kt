package com.example.core

import retrofit2.Retrofit
import retrofit2.converter.gson.GsonConverterFactory

object DataClients {
    private const val backUrl = "http://10.0.2.2:5058"

    val myBookClient: Retrofit = Retrofit.Builder()
        .baseUrl(backUrl)
        .addConverterFactory(GsonConverterFactory.create())
        .build()
}