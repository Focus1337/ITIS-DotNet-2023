package com.example.data.usecase

import com.example.data.datasource.DataSources
import com.example.domain.dto.UserData
import com.example.domain.usecase.IUserUseCase
import retrofit2.awaitResponse


class UserUseCase : IUserUseCase {
    override suspend fun getUser(id: Int): UserData? {
        if (id == 0) {
            return null
        }
        return DataSources
            .userService
            .getUserData(id)
            .awaitResponse()
            .body()
    }
}
