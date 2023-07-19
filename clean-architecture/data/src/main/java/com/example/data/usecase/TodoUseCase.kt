package com.example.data.usecase

import com.example.data.datasource.DataSources
import com.example.domain.dto.TodoData
import com.example.domain.usecase.ITodoUseCase
import retrofit2.awaitResponse

class TodoUseCase : ITodoUseCase {

    override suspend fun getTodo(id: Int): TodoData? {
        if (id == 0) {
            return null
        }
        return DataSources
            .todoService
            .getTodoData(id)
            .awaitResponse()
            .body()
    }
}