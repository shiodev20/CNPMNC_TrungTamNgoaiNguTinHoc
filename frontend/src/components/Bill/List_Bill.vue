<template>
  <div class="list_exam">
    <h1>Danh sách hóa đơn</h1>
    <div class="Upperbar">
      <div class="filter_customer">
        <label class="lb_filter_cus">
          <input v-model="choice_type" type="radio" :value="1" name="dd" />
          <span> Khóa học <strong></strong></span>
        </label>
        <label class="lb_filter_cus">
          <input v-model="choice_type" type="radio" :value="2" name="dd" />
          <span>kỳ thi <strong></strong></span>
        </label>
      </div>
    </div>
    <div v-if="list_branch" class="list_exam_child">
      <!-- filter  -->
      <div class="parent_filter">
        <div class="filer_list_exam">
          <div class="filter_course">
            <input
              type="text"
              v-model="key_search"
              id="ipname"
              placeholder="email"
            />
            <div class="dropdown">
              <div
                @click="status_dropdown_course = !status_dropdown_course"
                class="dropdown_input"
              >
                <span v-if="choice_type == 1" class="text">
                  {{
                    data_course.id_course == null
                      ? "Chọn khóa học"
                      : course_id(data_course.id_course) == null
                      ? null
                      : course_id(data_course.id_course).name
                  }}
                </span>
                <span v-if="choice_type == 2" class="text">
                  {{
                    data_exam.id_exam == null
                      ? "Chọn kỳ thi"
                      : exam_id(data_exam.id_exam) == null
                      ? null
                      : exam_id(data_exam.id_exam).name
                  }}
                </span>
                <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
              </div>
              <div
                :class="
                  status_dropdown_course == true ? 'dropdown_menu_show' : null
                "
                class="dropdown_menu"
              >
                <div
                  @click="
                    choice_type == 1
                      ? ((data_course.id_course = item.id),
                        (status_dropdown_course = false))
                      : ((data_exam.id_exam = item.id),
                        (status_dropdown_course = false))
                  "
                  class="dropdown_item"
                  v-for="item in choice_type == 1 ? course : exam"
                  :key="item.id"
                >
                  {{ item.name }}
                </div>
              </div>
            </div>

            <div
              v-if="
                choice_type == 1
                  ? data_course.id_course != null
                  : data_exam.id_exam != null
              "
              class="dropdown"
            >
              <div
                @click="status_dropdown_class = !status_dropdown_class"
                class="dropdown_input"
              >
                <span v-if="choice_type == 1" class="text">
                  {{
                    data_course.id_class == null
                      ? "Chọn lớp học "
                      : class_id(data_course.id_class).name
                  }}
                </span>
                <span v-if="choice_type == 2" class="text">
                  {{
                    data_exam.id_exambatch == null
                      ? "Chọn đợt thi "
                      : exambatch_id(data_exam.id_exambatch).name
                  }}
                </span>
                <font-awesome-icon class="icon" :icon="['fas', 'caret-down']" />
              </div>
              <div
                :class="
                  status_dropdown_class == true ? 'dropdown_menu_show' : null
                "
                class="dropdown_menu"
              >
                <div
                  @click="
                    choice_type == 1
                      ? ((data_course.id_class = item.id),
                        (status_dropdown_class = false))
                      : ((data_exam.id_exambatch = item.id),
                        (status_dropdown_class = false))
                  "
                  class="dropdown_item"
                  v-for="item in choice_type == 1
                    ? class_idcourse(data_course.id_course)
                    : exam_idexambatch(data_exam.id_exam)"
                  :key="item.id"
                >
                  {{ item.name }}
                </div>
              </div>
            </div>
            <button class="btnseach" @click="search()">Tìm kiếm</button>
          </div>
        </div>
        <div>
          <strong>Số lượng {{ list_branch.length }}</strong>
        </div>
      </div>
      <!-- end filter -->
      <!-- List exam -->
      <div v-if="list_branch" class="list_exams">
        <table v-if="list_branch.length > 0" class="table">
          <tr>
            <th>Id</th>
            <th>Khách hàng</th>
            <th>Số tiền</th>
            <th>Mã hóa đơn</th>
            <th>Ngày thanh toán</th>
            <th>Loại</th>
            <th>Chi tiết</th>
          </tr>
          <tr :id="'item' + item.id" v-for="item in list_branch" :key="item.id">
            <td>{{ item.id }}</td>
            <td>
              {{
                cus_id(item.id_cus).lastname +
                " " +
                cus_id(item.id_cus).firtname
              }}
            </td>
            <td>{{ item.price }}</td>
            <td>{{ item.id }}</td>
            <td>{{ convert_time(item.date_create) }}</td>
            <td v-if="exam">
              {{
                choice_type == 1
                  ? course_id(class_id(item.id_class).course_id).name
                  : exam_id(exambatch_id(item.id_exambactch).exam_id).name
              }}
            </td>
            <td v-if="exam">
              {{
                choice_type == 1
                  ? class_id(item.id_class).name
                  : exambatch_id(item.id_exambactch).name
              }}
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

    for (var item in this.list_branch) {
      const div = document.getElementById("item" + this.list_branch[item].id);
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
    store.dispatch("branch/get_all");
    store.dispatch("bill_cus_class/get_all");
    store.dispatch("bill_cus_exam/get_all");
    store.dispatch("acount/get_all");
    store.dispatch("course/get_all");
    store.dispatch("class/get_all");
    store.dispatch("exam/get_all");
    store.dispatch("exambatch/get_all");
    const class_idcourse = (value) => {
      return store.getters["class/getbyidcourse"](value);
    };
    const exam_idexambatch = (value) => {
      return store.getters["exambatch/getbyidexam"](value);
    };
    const exambatch_id = (value) => {
      return store.getters["exambatch/getbyid"](value)[0];
    };
    const class_id = (value) => {
      return store.getters["class/getbyid"](value)[0];
    };
    const exam_id = (value) => {
      return store.getters["exam/getbyid"](value);
    };
    const course = computed(() => {
      return store.state.course.course;
    });
    const exam = computed(() => {
      return store.state.exam.exam;
    });
    const cus_id = (value) => {
      return store.getters["acount/getbyid"](value)[0];
    };
    const course_id = (value) => {
      return store.getters["course/getbyid"](value)[0];
    };

    const list_branch = computed(() => {
      if (ssearch.value == true) {
        const cus = store.getters["acount/getbyemail"](key_search.value)[0];
        if (cus == undefined) {
          return [];
        }
        if (choice_type.value == 1) {
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          ssearch.value = false;
          return store.getters["bill_cus_class/getbyids"](
            cus.id,
            data_course.id_class
          );
        } else {
          // eslint-disable-next-line vue/no-side-effects-in-computed-properties
          ssearch.value = false;
          return store.getters["bill_cus_exam/getbyids"](
            cus.id,
            data_exam.id_exambatch
          );
        }
      }
      if (choice_type.value == 2) {
        return store.state.bill_cus_exam.bill_cus_exam;
      }
      return store.state.bill_cus_class.bill_cus_class;
    });
    const deletes = (id) => {
      store.dispatch("branch/delete", id);
    };

    const this_page = ref(1);
    const limit_page = ref(6);
    const toast = useToast();
    const run_app = watch(this_page, () => {
      let page_start = limit_page.value * (this_page.value - 1);
      let page_end = limit_page.value * this_page.value - 1;
      for (var item in list_branch.value) {
        const div = document.getElementById(
          "item" + list_branch.value[item].id
        );
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
      if (list_branch.value == null) {
        return 0;
      }
      return Math.ceil(list_branch.value.length / limit_page.value);
    });

    const chage = (item) => {
      this_page.value = item;
    };

    const key_search = ref("");
    const key_search_ap = ref(null);
    const choice_type = ref(1);
    const status_dropdown_course = ref(null);
    const status_dropdown_class = ref(null);

    const data_course = reactive({
      id_course: null,
      id_class: null,
    });
    const data_exam = reactive({
      id_exam: null,
      id_exambatch: null,
    });
    const convert_time = (value) => {
      const x = new Date(value);
      return (
        x.getHours() +
        ":" +
        x.getMinutes() +
        "  " +
        x.getDate() +
        "/" +
        x.getMonth() +
        "/" +
        x.getFullYear()
      );
    };
    const ssearch = ref(false);
    const search = () => {
      if (choice_type.value == 1) {
        if (
          data_course.id_class != null &&
          data_course.id_course != null &&
          key_search.value != ""
        ) {
          ssearch.value = true;
        } else {
          toast.error("Không hợp lệ");
        }
      } else {
        if (
          data_exam.id_exam != null &&
          data_exam.id_exambatch != null &&
          key_search.value != ""
        ) {
          ssearch.value = true;
        } else {
          toast.error("Không hợp lệ");
        }
      }
    };
    return {
      data_course,
      list_branch,
      deletes,
      count_page,
      chage,
      this_page,
      limit_page,
      run_app,
      key_search,
      key_search_ap,
      cus_id,
      course_id,
      choice_type,
      course,
      status_dropdown_course,
      status_dropdown_class,
      class_idcourse,
      class_id,
      exam,
      data_exam,
      exam_id,
      exambatch_id,
      exam_idexambatch,
      convert_time,
      search,
    };
  },
};
</script>
<style scoped>
.filter_customer {
  display: flex;
  gap: 10px;
}
.lb_filter_cus > input {
  display: none;
}
.lb_filter_cus > span {
  padding: 10px;
  border: 2px solid #071952;
  border-radius: 10px;
  color: #071952;
}
.lb_filter_cus > input:checked ~ span {
  color: rgb(203, 203, 203);
  background: #071952;
}
.parent_filter {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
.btnseach {
  border-radius: 5px;
  border: none;
  background: var(--color_main);
  color: white;
}
#ipname {
  border: 1px solid var(--color_main);
  border-radius: 5px;
  padding-left: 10px;
}
.filer_list_exam > div {
  display: flex;
  gap: 10px;
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
.ip_name_exambatch {
  padding-left: 10px;
}
/* pagenigation */
.group_page {
  position: absolute;
  display: flex;
  bottom: 15px;
  right: 30px;
  gap: 10px;
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
  color: rgb(85, 250, 85);
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
  gap: 10px;
  padding: 10px;
  padding-left: 0;
}
.filer_list_exam > input {
  border-radius: 10px;
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
  position: relative;
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
  width: 1000px;
  height: 72px;
  padding: 5px 15px;
  justify-content: space-between;
  align-items: center;
  flex-shrink: 0;
  background: white;
  border-radius: 10px;
}
.Upperbar_add > button {
  width: 146px;
  height: 33px;
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
.field {
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
