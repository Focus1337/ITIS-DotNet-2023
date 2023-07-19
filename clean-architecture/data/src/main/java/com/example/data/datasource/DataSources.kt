package com.example.data.datasource

import com.example.core.RetrofitClient
import com.example.domain.datasource.ITodoDataSource
import com.example.domain.datasource.IUserDataSource

class DataSources {
    companion object {
        val todoService: ITodoDataSource =
            RetrofitClient.retrofitClient.create(ITodoDataSource::class.java)

        val userService: IUserDataSource =
            RetrofitClient.retrofitClient.create(IUserDataSource::class.java)
    }
}