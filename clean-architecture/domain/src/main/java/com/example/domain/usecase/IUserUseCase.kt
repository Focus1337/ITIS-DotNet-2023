package com.example.domain.usecase

import com.example.domain.dto.UserData

interface IUserUseCase {
    suspend fun getUser(id: Int): UserData?
}