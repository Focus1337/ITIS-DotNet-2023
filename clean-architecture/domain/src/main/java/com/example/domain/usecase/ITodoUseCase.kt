package com.example.domain.usecase

import com.example.domain.dto.TodoData

interface ITodoUseCase {
    suspend fun getTodo(id: Int): TodoData?
}