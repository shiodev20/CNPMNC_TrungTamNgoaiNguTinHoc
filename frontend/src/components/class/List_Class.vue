<template>
  <div class="list_exam">
    <h1>Danh sách lớp học</h1>
    <div class="Upperbar">
      <div
        v-if="$store.state.acount.authencation.role == 'admin'"
        class="Upperbar_add"
      >
        <button
          @click="
            this.$router.push({
              name: 'class',
              query: { action: 'create' },
            })
          "
        >
          Tạo lớp học
        </button>
      </div>
      <div class="upper_parent">
        <div class="name_exambatch">
          <label>Tên lớp học</label>
          <input
            v-model="data_get.name"
            type="text"
            class="ip_name_exambatch field"
          />
          <span v-if="err.name" class="err">{{ err.name }}</span>
        </div>
        <div class="name_exambatch">
          <label>Ngày bắt đầu</label>
          <input v-model="data_get.date_start" type="date" />
          <span v-if="err.date_start" class="err">{{ err.date_start }}</span>
        </div>
        <div class="name_exambatch">
          <label>Ngày kết thúc</label>
          <input v-model="data_get.date_end" type="date" />
          <span v-if="err.date_end" class="err">{{ err.date_end }}</span>
        </div>
        <div class="btn_search">
          <button @click="search">Tìm kiếm</button>
        </div>
      </div>
    </div>
    <div v-if="list_class" class="list_exam_child">
      <!-- filter  -->
      <div class="parent_filter">
        <div class="filer_list_exam">
          <div class="dropdown">
            <div
              @click="status_dropdown = !status_dropdown"
              class="dropdown_input"
            >
              <span class="text">
                {{
                  data_fetch.name == null ? "Chọn khóa học" : data_fetch.name
                }}</span
              >
              <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
            </div>
            <div
              :class="status_dropdown == true ? 'dropdown_menu_show' : null"
              class="dropdown_menu"
            >
              <div
                @click="
                  (data_fetch.name = item.name),
                    (data_fetch.id = item.id),
                    (status_dropdown = false)
                "
                class="dropdown_item"
                v-for="item in data"
                :key="item.id"
              >
                {{ item.name }}
              </div>
            </div>
          </div>
          <div class="btn_filter_parent">
            <button class="btn_filter" @click="chose_key = data_fetch.id">
              Lọc
            </button>
          </div>
        </div>
        <div>
          <strong>Số lượng {{ list_class.length }}</strong>
        </div>
      </div>
      <!-- end filter -->
      <!-- List exam -->
      <div v-if="list_class" class="list_exams">
        <table v-if="list_class.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Tên lớp học</th>
            <th>Ngày bắt đầu</th>
            <th>Ngày kết thúc</th>
            <th>Khóa học</th>
            <th>Chức năng</th>
          </tr>
          <tr :id="'item' + item.id" v-for="item in list_class" :key="item.id">
            <td>{{ item.id }}</td>
            <td>{{ item.name }}</td>
            <td>{{ item.start_date }}</td>
            <td>{{ item.end_date }}</td>
            <td>
              {{
                course_id(item.course_id) == null
                  ? null
                  : course_id(item.course_id).name
              }}
            </td>
            <td class="setting">
              <button
                @click="
                  this.$router.push({
                    name: 'class',
                    query: { action: 'detail', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_detail"
                  :icon="['fas', 'people-group']"
                />
              </button>
              <button
                @click="
                  this.$router.push({
                    name: 'class',
                    query: { action: 'detail-lesson', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_detail"
                  :icon="['fas', 'list']"
                />
              </button>
              <button
                v-if="$store.state.acount.authencation.role == 'admin'"
                @click="
                  this.$router.push({
                    name: 'class',
                    query: { action: 'edit', id: item.id },
                  })
                "
              >
                <font-awesome-icon
                  class="icon_edit"
                  :icon="['fas', 'pen-fancy']"
                />
              </button>
              <button
                v-if="$store.state.acount.authencation.role == 'admin'"
                @click="deletes(item.id)"
              >
                <font-awesome-icon
                  class="icon_trash"
                  :icon="['fas', 'trash-can']"
                />
              </button>
            </td>
          </tr>
        </table>
        <div class="images" v-else>
          <img src="../Imagess/notfound.png" alt="" />
        </div>
      </div>
      <!-- end list exam -->
      <!-- pagnigation -->
      <div class="group_page">
        <div
          @click="chage(this_page - 1)"
          v-if="this_page > 1 && count_page > 0"
          class="pre"
        >
          <font-awesome-icon :icon="['fas', 'arrow-left']" />
        </div>
        <div
          :class="this_page == item ? 'show_item_page' : null"
          @click="chage(item)"
          v-for="item in count_page"
          :key="item"
          class="item_page"
        >
          {{ item }}
        </div>
        <div
          @click="chage(this_page + 1)"
          v-if="this_page < count_page"
          class="next"
        >
          <font-awesome-icon :icon="['fas', 'arrow-right']" />
        </div>
      </div>
      <!-- end pagnigation -->
    </div>
  </div>
</template>
<script>
import { computed, reactive, ref, watch } from "vue";
import store from "@/store";
import { useToast } from "vue-toastification";
export default {
  name: "HomeView",
  updated() {
    let page_start = this.limit_page * (this.this_page - 1);
    let page_end = this.limit_page * this.this_page - 1;

    for (var item in this.list_class) {
      const div = document.getElementById("item" + this.list_class[item].id);
      if (item >= page_start && item <= page_end) {
        div.classList.contains("hide") ? div.classList.remove("hide") : null;
        div.classList.add("show");
      } else {
        div.classList.contains("show") ? div.classList.remove("show") : null;
        div.classList.add("hide");
      }
    }
  },
  setup() {
    const toast = useToast();
    const err = reactive({
      name: null,
      date_start: null,
      date_end: null,
    });
    const data_get = reactive({
      name: null,
      date_start: null,
      date_end: null,
    });
    const deletes = (id) => {
      toast.error("Xóa Thành công");
      store.dispatch("class/delete", id);
    };
    store.dispatch("course/get_all");
    store.dispatch("class/get_all");
    const data_fetch = reactive({
      name: null,
      id: null,
    });
    const status_dropdown = ref(false);
    const data = computed(() => {
      return store.state.course.course;
    });

    const this_page = ref(1);
    const limit_page = ref(6);

    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_class.value) {
        const div = document.getElementById("item" + list_class.value[item].id);
        if (item >= page_start && item <= page_end) {
          div.classList.contains("hide") ? div.classList.remove("hide") : null;
          div.classList.add("show");
        } else {
          div.classList.contains("show") ? div.classList.remove("show") : null;
          div.classList.add("hide");
        }
      }
    });

    const count_page = computed(() => {
      if (list_class.value == null) {
        return 0;
      }
      return Math.ceil(list_class.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
    };

    const list_class = computed(() => {
      if (chose_key.value != null) {
        return store.state.class.class.filter(
          (e) => e.course_id == chose_key.value
        );
      }
      if (date_post.name != null) {
        const date_start = new Date(date_post.date_start);
        const date_end = new Date(date_post.date_end);
        return store.state.class.class.filter(
          (e) =>
            e.name.toUpperCase().includes(date_post.name.toUpperCase()) &&
            new Date(e.start_date).getTime() >= date_start.getTime() &&
            new Date(e.end_date).getTime() <= date_end.getTime()
        );
      }
      return store.state.class.class;
    });
    const chose_key = ref(null);

    const date_post = reactive({
      name: null,
      date_start: null,
      date_end: null,
    });
    const search = () => {
      data_get.name == null || data_get.name == ""
        ? (err.name = "Không bỏ trống")
        : (err.name = null);
      data_get.date_start == null
        ? (err.date_start = "Không bỏ trống")
        : (err.date_start = null);
      data_get.date_end == null
        ? (err.date_end = "Không bỏ trống")
        : (err.date_end = null);
      if (
        err.date_end == null &&
        err.date_start == null &&
        err.date_end == null
      ) {
        chose_key.value = null;
        date_post.name = data_get.name;
        date_post.date_start = data_get.date_start;
        date_post.date_end = data_get.date_end;
      }
    };
    const course_id = (value) => {
      return store.getters["course/getbyid"](value)[0];
    };
    return {
      data_get,
      search,
      chose_key,
      data,
      data_fetch,
      status_dropdown,
      list_class,
      deletes,
      count_page,
      limit_page,
      this_page,
      chage,
      run_app,
      err,
      course_id,
    };
  },
};
</script>
<style scoped>
.parent_filter {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.err {
  position: absolute;
  color: red;
  font-size: 12px;
  top: 100%;
  left: 0;
}
.upper_parent > div {
  position: relative;
}
.show_item_page {
  background: var(--color_table);
  color: white !important;
}
.hide {
  display: none;
}
.show {
  display: table-row !important;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
}
.group_page > div {
  color: var(--color_table);
  width: 30px;
  height: 30px;
  border-radius: 50%;
  display: flex;
  justify-content: center;
  align-items: center;
  color: var(--color_table);
}

.item_page:hover {
  background: var(--color_table);
  color: white;
}
.pre,
.next {
  border: 1px solid var(--color_table);
}

/* end pagnagation */
/* list exam  */
table {
  border-collapse: collapse;
  width: 100%;
}
.tale {
  margin-left: auto;
  margin-right: auto;
}

th,
td {
  padding: 8px;
  text-align: center;
  vertical-align: middle !important;
}

tr:nth-child(even) {
  background-color: var(--color_row);
}

th {
  background-color: var(--color_table);
  color: white;
}

.setting > button {
  border-radius: 20px;
  background: white;
  border: none;
  margin-left: 10px;
  box-shadow: 0px 4px 4px 0px rgba(0, 0, 0, 0.25);
}

.icon_trash,
.icon_edit,
.icon_detail {
  font-size: 20px;
  padding: 5px;
}
.icon_trash {
  color: red;
}
.icon_edit {
  color: var(--color_edit);
}

/* end list exam  */
/* filter list */
.btn_filter_parent {
  display: flex;
  align-items: center;
  justify-content: center;
}
.filer_list_exam {
  display: flex;
}
.btn_filter {
  border-radius: 10px;
  padding: 10px;
  background: var(--color_main);
  color: white;
}
/* end filter list */
/* drop down  */
.dropdown_input > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_input > .text,
.dropdown_input_from > .text {
  position: absolute;
  top: 50%;
  left: 50%;
  color: #071952;
  text-align: center;
  font-family: Roboto;
  font-size: 15px !important;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
  transform: translate(-50%, -50%);
  white-space: nowrap;
}
.dropdown_menu_show {
  opacity: 1 !important;
  pointer-events: visible !important;
}
.dropdown {
  width: 180px;
  margin: 10px;
  position: relative;
  margin-left: 0;
}
.dropdown_input {
  position: relative;
  padding: 20px 20px;
  background: white;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  color: var(--color_main);
  font-weight: 600;
}
.dropdown_menu {
  transition: 0.1s;
  margin-top: 3px;
  border: 1px solid var(--color_main);
  position: absolute;
  left: 0;
  right: 0;
  opacity: 0;
  z-index: 1;
  pointer-events: none;
}
.dropdown_item {
  padding: 10px;
  background: white;
}
.dropdown_item:hover {
  background: var(--color_main);
  color: white;
}
/* end drop down  */

h1 {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 24px;
  font-style: normal;
  font-weight: 600;
  line-height: normal;
}

/* upperbar */
.Upperbar {
  display: flex;
  width: 912px;
  height: 72px;
  padding: 10px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
}
.Upperbar_add > button {
  padding: 10px 20px;
  color: white;
  border-radius: 10px;
  background: var(--color_main);
}
/* end upperbar */
/*
upper parent right  */
.btn_search > button {
  height: 33px !important;
  border-radius: 10px;
  background: var(--color_main);
  font-size: 12px !important;
  color: white;
}
.upper_parent {
  display: flex;
  gap: 10px;
}
.upper_parent > div {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: flex-start;
  gap: 5px;
}
.btn_search {
  display: flex;
  justify-content: end !important;
  border-radius: 10px;
}
.upper_parent > div > label {
  color: var(--color_main);
  text-align: center;
  font-family: Roboto;
  font-size: 12px;
  font-style: normal;
  font-weight: 500;
  line-height: normal;
}
input {
  padding-left: 10px;
  width: 146px !important;
  height: 33px !important;
  border-radius: 10px;
  border: 1px solid var(--color_main);
  flex-shrink: 0;
  position: relative;
}
.field > .icon {
  position: absolute;
  right: 10px;
  top: 50%;
  transform: translateY(-50%);
}
.dropdown_from {
  position: relative;
}
/* end uppper parent right */

/* list parent  */
.list_exam_child {
  background: white;
  border-radius: 10px;
  padding: 0 20px;
  margin-top: 19px;
  padding-bottom: 67px;
  position: relative;
  height: var(--height_from_admin);
}
/* end list parent  */
</style>
